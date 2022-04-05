using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EGS.Application.Common.Interfaces;
using EGS.Infrastructure.Identity;
using EGS.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using System.Linq.Expressions;
using Respawn.Graph;
using System.Threading;

namespace EGS.Application.UnitTests
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfigurationRoot config;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;
        private static string _currentUserId;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            config = builder.Build();
            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "EGS.Api"));

            services.AddLogging();

            EGS.Application.Setup.Configure(services, config);
            EGS.Infrastructure.Setup.Configure(services, config);
            EGS.Api.Setup.Configure(services, config);


            // Replace service registration for ICurrentUserService
            // Remove existing registration
            var currentUserServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            services.Remove(currentUserServiceDescriptor);

            // Register testing version
            services.AddTransient(_ =>
                Mock.Of<ICurrentUserService>(s => s.UserId == _currentUserId));

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new Table[] { "__EFMigrationsHistory" }
            };

            EnsureDatabase();
        }

        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Database.Migrate();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<ISender>();

            return await mediator.Send(request);
        }

        public static async Task<string> RunAsDefaultUserAsync()
        {
            return await RunAsUserAsync("customer@egs.com", "Egs_2023@)", new[] { "Customer" });
        }

        public static async Task<string> RunAsAdministratorAsync()
        {
            return await RunAsUserAsync("admin@egs.com", "Egs_2023@)", new[] { "Admin" });
        }

        public static async Task<string> RunAsUserAsync(string userName, string password, string[] roles)
        {            
            var userId = await AddUser(userName, password, roles);

            if (!string.IsNullOrEmpty(userId))
                _currentUserId = userId;

            return userId;
        }

        public static async Task<string> AddUser(string userName, string password, string[] roles)
        {
            var scope = _scopeFactory.CreateScope();
            var userManager = scope.ServiceProvider.GetService<UserManager<EGS.Infrastructure.Identity.ApplicationUser>>();

            var user = new EGS.Infrastructure.Identity.ApplicationUser { UserName = userName, Email = userName };

            var result = await userManager.CreateAsync(user, password);

            if (roles.Any())
            {
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                await userManager.AddToRolesAsync(user, roles);
            }

            if(!result.Succeeded)
            {
                var errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors);
                throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");
            }
            return user.Id;
        }

        public static async Task ResetState()
        {
            await _checkpoint.Reset(config.GetConnectionString("DefaultConnection"));
            _currentUserId = null;
        }

        public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        public static async Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            return await context
                .Set<TEntity>()
                .AsQueryable()
                .CountAsync(predicate);                
        }

        public static async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            return await context
                .Set<TEntity>()
                .AsQueryable()
                .AnyAsync(predicate);            
        }

        public static async Task<TEntity> AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync(CancellationToken.None);

            return entity;
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}
