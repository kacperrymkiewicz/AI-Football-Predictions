using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AI.Football.Predictions.Integrations.Configurations
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationSection GetApiProviderSection(this IConfiguration configuration, string providerName)
        {
            return configuration.GetSection($"ApiProviders:{providerName}");
        }
    }
}