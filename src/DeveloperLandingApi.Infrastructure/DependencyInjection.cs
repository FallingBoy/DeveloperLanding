using DeveloperLandingApi.Application.Interfaces;
using DeveloperLandingApi.Infrastructure.AI;
using DeveloperLandingApi.Infrastructure.Email;
using DeveloperLandingApi.Infrastructure.Options;
using DeveloperLandingApi.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace DeveloperLandingApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<OpenRouterOptions>(configuration.GetSection(OpenRouterOptions.SectionName));
            services.Configure<SmtpOptions>(configuration.GetSection(SmtpOptions.SectionName));
            services.AddHttpClient<IAiAnalyzer, OpenAiAnalyzer>();
            services.AddScoped<IContactRepository, JsonContactRepository>();
            services.AddScoped<IEmailSender, SmtpEmailSender>();

            return services;
        }
    }
}
