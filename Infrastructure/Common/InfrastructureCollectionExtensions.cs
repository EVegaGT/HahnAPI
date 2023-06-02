using Infrastructure.Repositories;
using Infrastructure.Repositories.ReadOnlyRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common
{
    public static class InfrastructureCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Map User Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();

            // Map Rol Repositories
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleReadOnlyRepository, RoleReadOnlyRepository>();

            // Map Brand Repositories
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IBrandReadOnlyRepository, BrandReadOnlyRepository>();

            // Map Category Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryReadOnlyRepository, CategoryReadOnlyRepository>();

            // Map Product Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductReadOnlyRepository, ProductReadOnlyRepository>();

            // Map Order Repositories
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderReadOnlyRepository, OrderReadOnlyRepository>();

            return services;
        }
    }
}
