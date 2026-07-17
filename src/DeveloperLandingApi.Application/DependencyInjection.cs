using DeveloperLandingApi.Application.Interfaces;
using DeveloperLandingApi.Application.Options;
using DeveloperLandingApi.Application.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DeveloperLandingApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContactService, ContactService>();
            services.Configure<ContactSettings>(configuration.GetSection(ContactSettings.SectionName));
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            services.AddScoped<IMetricsService, MetricsService>();

            return services;
        }

    }
}
