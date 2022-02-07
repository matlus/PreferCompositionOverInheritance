using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositionOverInheritance.Core
{
    internal sealed class CosmosDbSettingsProvider
    {
        private readonly ConfigurationSectionRetriever configurationSectionRetriever;

        public CosmosDbSettingsProvider(ConfigurationSectionRetriever configurationSectionRetriever)
        {
            this.configurationSectionRetriever = configurationSectionRetriever;
        }

        private CosmosDbSettingsConfig GetCosmosDbSettingsPreValidated()
        {
            const string messageBrokerSettingsKey = "CosmosDb";
            var cosmosDbSettingsConfig = configurationSectionRetriever.GetSection(messageBrokerSettingsKey).Get<CosmosDbSettingsConfig>();

            if (cosmosDbSettingsConfig == null)
            {
                cosmosDbSettingsConfig = new CosmosDbSettingsConfig();
            }

            return cosmosDbSettingsConfig;
        }

        public CosmosDbSettings GetCosmosDbSettings()
        {
            var cosmosDbSettingsConfig = GetCosmosDbSettingsPreValidated();
            ValidatorCosmosDbSettingsConfig.Validate(cosmosDbSettingsConfig);
            return MapperCosmosDbSettingsConfig.MapToCosmosDbSettings(cosmosDbSettingsConfig);
        }
    }
}
