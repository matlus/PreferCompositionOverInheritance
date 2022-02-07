namespace CompositionOverInheritance.Core
{
    internal static class MapperCosmosDbSettingsConfig
    {
        public static CosmosDbSettings MapToCosmosDbSettings(CosmosDbSettingsConfig cosmosDbSettingsConfig)
        {
            return new CosmosDbSettings(cosmosDbSettingsConfig.DatabaseName, cosmosDbSettingsConfig.CosmosDbConnectionString);
        }
    }
}
