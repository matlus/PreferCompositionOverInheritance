using Microsoft.Extensions.Configuration;

namespace CompositionOverInheritance.Core
{
    internal sealed class StorageAccountSettingsProvider
    {
        private readonly ConfigurationSectionRetriever configurationSectionRetriever;

        public StorageAccountSettingsProvider(ConfigurationSectionRetriever configurationSectionRetriever)
        {
            this.configurationSectionRetriever = configurationSectionRetriever;
        }

        private StorageAccountSettingsConfig GetStorageAccountSettingsPreValidated()
        {
            const string storageAccountSettingsKey = "StorageAccount";
            var storageAccountSettingsConfig = configurationSectionRetriever.GetSection(storageAccountSettingsKey).Get<StorageAccountSettingsConfig>();
            storageAccountSettingsConfig.MonitorImageContainerName = "alive-screenshots";
            if (storageAccountSettingsConfig != null)
            {
                return storageAccountSettingsConfig;
            }
            else
            {
                storageAccountSettingsConfig = new StorageAccountSettingsConfig();
            }

            return storageAccountSettingsConfig;
        }

        public StorageAccountSettings GetStorageAccountSettings()
        {
            var storageAccountSettingsConfig = GetStorageAccountSettingsPreValidated();
            ValidatorStorageAccountSettingsConfig.Validate(storageAccountSettingsConfig);
            return MapperStorageAccountSettingsConfig.MapToStorageAccountSettings(storageAccountSettingsConfig);
        }
    }
}
