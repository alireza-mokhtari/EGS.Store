using EGS.Api.Filters;
using EGS.Api.Services;
using EGS.Application.Common.Abstractions;
using FluentValidation.AspNetCore;

namespace EGS.Api
{
    public static class Setup
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>());

            services.AddFluentValidation();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
