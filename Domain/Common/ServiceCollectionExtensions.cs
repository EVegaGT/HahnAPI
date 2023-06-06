using Microsoft.Extensions.DependencyInjection;
using Domain.Helpers;
using FluentValidation;
using Domain.Models.Requests.Brand;
using Domain.Validations.Brand;
using Domain.Validations.User;
using Domain.Models.Requests.User;

namespace Domain.Common
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Map Helpers
            services.AddScoped<IJwtHelper, JwtHelper>();

            // User map+
            services.AddScoped<IValidator<LoginRequest>, LoginValidator>();
            services.AddScoped<IValidator<RegisterUserRequest>, RegisterValidator>();

            // Brand map
            services.AddScoped<IValidator<BrandRequest>, BrandValidator>();

            return services;
        }
    }
}
