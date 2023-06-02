using Microsoft.Extensions.DependencyInjection;

namespace Domain.Common
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
