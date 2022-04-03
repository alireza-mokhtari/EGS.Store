using EGS.Domain.Entities;
using EGS.Domain.Security;
using EGS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace EGS.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleIdentitiesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoleIfNotExists(roleManager, Constants.ADMIN_ROLE);
            await SeedRoleIfNotExists(roleManager, Constants.CUSTOMER_ROLE);

            await SeedUserIfNotExists(userManager, "Administrator", "admin", "admin@EGS.com", "EGS_2022", Constants.ADMIN_ROLE);
            await SeedUserIfNotExists(userManager, "Alireza Mokhtari", "alireza", "alireza@my-EGS.com", "EGS_2022", Constants.CUSTOMER_ROLE);
        }

        private static async Task SeedRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var role = new IdentityRole(roleName);

            if (roleManager.Roles.All(r => r.Name != role.Name))
                await roleManager.CreateAsync(role);
        }

        private static async Task SeedUserIfNotExists(UserManager<ApplicationUser> userManager, string fullName, string userName, string email, string password, string roleName)
        {
            var defaultUser = new ApplicationUser { UserName = userName, Email = email, FullName = fullName };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, password);
                await userManager.AddToRolesAsync(defaultUser, new[] { roleName });
            }
        }

        public static async Task SeedBookGenresAsync(ApplicationDbContext context)
        {
            var categories = new string[] { "Biography", "Business", "Science", "Romance", "History" };
            if (!context.BookGenres.Any())
            {
                foreach (var category in categories)
                {
                    context.BookGenres.Add(new BookGenre
                    {
                        Title = category
                    });
                }
                await context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}
