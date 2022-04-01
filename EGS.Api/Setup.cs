using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EGS.Api
{
    public static class Setup
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();            
            services.AddFluentValidation();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
