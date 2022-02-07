using CompositionOverInheritance.Core;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DomainLayer2
{
    internal sealed class ConfigurationProvider : ConfigurationProviderBase
    {
        private MessageBrokerSettingsProvider messageBrokerSettingsProvider;
        private CosmosDbSettingsProvider cosmosDbSettingsProvider;
        private StorageAccountSettingsProvider storageAccountSettingsProvider;

        public ConfigurationProvider()
            :base()
        {
            InitializeSettingsProviders();            
        }

        [ExcludeFromCodeCoverage]
        internal ConfigurationProvider(IConfigurationRoot configurationRoot)
            : base(configurationRoot)
        {
            InitializeSettingsProviders();
        }

        private void InitializeSettingsProviders()
        {
            messageBrokerSettingsProvider = new MessageBrokerSettingsProvider(ConfigurationSettingRetriever);
            cosmosDbSettingsProvider = new CosmosDbSettingsProvider(ConfigurationSettingRetriever);
            storageAccountSettingsProvider = new StorageAccountSettingsProvider(ConfigurationSettingRetriever);
        }

        public MessageBrokerSettings GetMessageBrokerSettings()
        {
            return messageBrokerSettingsProvider.GetMessageBrokerSettings();
        }

        public CosmosDbSettings GetCosmosDbSettings()
        {
            return cosmosDbSettingsProvider.GetCosmosDbSettings();
        }

        public StorageAccountSettings GetStorageAccountSettings()
        {
            return storageAccountSettingsProvider.GetStorageAccountSettings();
        }
    }
}
