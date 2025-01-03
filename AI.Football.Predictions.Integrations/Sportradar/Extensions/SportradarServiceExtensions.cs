using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Football.Predictions.Integrations.Configurations;
using AI.Football.Predictions.Integrations.Sportradar.Models;
using AI.Football.Predictions.Integrations.Sportradar.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AI.Football.Predictions.Integrations.Sportradar.Extensions
{
    public static class SportradarServiceExtensions
    {
        public static IServiceCollection AddSportradarService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SportradarSettings>(configuration.GetApiProviderSection("Sportradar"));
            services.AddHttpClient("Sportradar", client =>
            {
                var sportradarSettings = configuration.GetApiProviderSection("Sportradar").Get<SportradarSettings>();
                if (sportradarSettings == null || string.IsNullOrEmpty(sportradarSettings.ApiKey))
                {
                    throw new ArgumentNullException(nameof(sportradarSettings), "Sportradar settings not configured properly.");
                }

                client.BaseAddress = new Uri(sportradarSettings.BaseUrl);
                client.DefaultRequestHeaders.Add("X-Auth-Token", sportradarSettings.ApiKey);
            });

            services.AddScoped<ISportradarService, SportradarService>();

            return services;
        }
    }
}