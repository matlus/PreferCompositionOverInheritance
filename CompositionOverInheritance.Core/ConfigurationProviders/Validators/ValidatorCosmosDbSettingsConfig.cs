using System.Text;

namespace CompositionOverInheritance.Core
{
    internal static class ValidatorCosmosDbSettingsConfig
    {
        public static void Validate(CosmosDbSettingsConfig cosmosDbSettingsConfig)
        {
            var errorMessages = new StringBuilder();

            ValidateCosmosDbSettingsConfig(cosmosDbSettingsConfig, errorMessages);

            if (errorMessages.Length != 0) throw new ConfigurationSettingMissingException(errorMessages.ToString());
        }

        private static void ValidateCosmosDbSettingsConfig(CosmosDbSettingsConfig cosmosDbSettingsConfig, StringBuilder errorMessages)
        {
            errorMessages.AppendLineIfNotNull(ValidatorString.Validate("CosmosDb.DatabaseName", cosmosDbSettingsConfig.DatabaseName));
            errorMessages.AppendLineIfNotNull(ValidatorString.Validate("CosmosDb.CosmosDbConnectionString", cosmosDbSettingsConfig.CosmosDbConnectionString));
        }
    }
}
