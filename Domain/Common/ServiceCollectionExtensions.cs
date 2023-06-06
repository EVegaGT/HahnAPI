using Microsoft.Extensions.DependencyInjection;
using Domain.Helpers;

namespace Domain.Common
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Map Helpers
            services.AddScoped<IJwtHelper, JwtHelper>();

            return services;
        }
    }
}
