using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI.Football.Predictions.Integrations.FootballData.Services;
using AI.Football.Predictions.Integrations.FootballData.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AI.Football.Predictions.Integrations.Configurations;

namespace AI.Football.Predictions.Integrations.FootballData.Extensions
{
    public static class FootballDataServiceExtensions
    {
        public static IServiceCollection AddFootballDataService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FootballDataSettings>(configuration.GetApiProviderSection("FootballData"));
            services.AddHttpClient("FootballData", client =>
            {
                var footballDataSettings = configuration.GetApiProviderSection("FootballData").Get<FootballDataSettings>();
                if (footballDataSettings == null || string.IsNullOrEmpty(footballDataSettings.ApiKey))
                {
                    throw new ArgumentNullException(nameof(footballDataSettings), "FootballData settings not configured properly.");
                }

                client.BaseAddress = new Uri(footballDataSettings.BaseUrl);
                client.DefaultRequestHeaders.Add("X-Auth-Token", footballDataSettings.ApiKey);
            });

            services.AddScoped<IFootballDataService, FootballDataService>();

            return services;
        }
    }
}
