using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DomainLayer
{
    internal sealed class ConfigurationProvider
    {
        private readonly IConfigurationRoot _configurationRoot;

        public ConfigurationProvider()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            LoadEnvironmentSpecificAppSettings(configurationBuilder);
            configurationBuilder.AddEnvironmentVariables("Alive_");

            _configurationRoot = configurationBuilder.Build();
        }

        private static void LoadEnvironmentSpecificAppSettings(ConfigurationBuilder configurationBuilder)
        {
            var aspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (aspNetCoreEnvironment != null)
            {
                configurationBuilder.AddJsonFile($"appsettings.{aspNetCoreEnvironment}.json");
            }
        }

        [ExcludeFromCodeCoverage]
        internal ConfigurationProvider(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        private string RetrieveConfigurationSettingValue(string key)
        {
            return _configurationRoot[key];
        }

        private MessageBrokerType GetMessageBrokerType()
        {
            var messageBrokerTypeString = RetrieveConfigurationSettingValueOrNull($"MessageBroker:MessageBrokerType") ?? "ServiceBus";
            var messageBrokerType = (MessageBrokerType)Enum.Parse(typeof(MessageBrokerType), messageBrokerTypeString);
            return messageBrokerType;
        }

        private string RetrieveConfigurationSettingValueOrNull(string key)
        {
            var value = RetrieveConfigurationSettingValue(key);
            switch (ValidatorString.DetermineNullEmptyOrWhiteSpaces(value))
            {
                case StringState.Null:
                case StringState.Empty:
                case StringState.WhiteSpaces:
                    return null;
                case StringState.Valid:
                default:
                    return value;
            }
        }

        private MessageBrokerSettingsConfig GetMessageBrokerSettingsPreValidated()
        {
            const string messageBrokerSettingsKey = "MessageBroker";
            var messageBrokerSettingsConfig = _configurationRoot.GetSection(messageBrokerSettingsKey).Get<MessageBrokerSettingsConfig>();

            if (messageBrokerSettingsConfig != null)
            {
                messageBrokerSettingsConfig.MessageBrokerType = GetMessageBrokerType();
            }
            else
            {
                messageBrokerSettingsConfig = new MessageBrokerSettingsConfig();
            }

            return messageBrokerSettingsConfig;
        }

        private CosmosDbSettingsConfig GetCosmosDbSettingsPreValidated()
        {
            const string messageBrokerSettingsKey = "CosmosDb";
            var cosmosDbSettingsConfig = _configurationRoot.GetSection(messageBrokerSettingsKey).Get<CosmosDbSettingsConfig>();

            if (cosmosDbSettingsConfig == null)
            {
                cosmosDbSettingsConfig = new CosmosDbSettingsConfig();
            }

            return cosmosDbSettingsConfig;
        }

        private StorageAccountSettingsConfig GetStorageAccountSettingsPreValidated()
        {
            const string storageAccountSettingsKey = "StorageAccount";
            var storageAccountSettingsConfig = _configurationRoot.GetSection(storageAccountSettingsKey).Get<StorageAccountSettingsConfig>();
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

        public MessageBrokerSettings GetMessageBrokerSettings()
        {
            var messageBrokerSettingsConfig = GetMessageBrokerSettingsPreValidated();
            ValidatorMessageBrokerSettingsConfig.Validate(messageBrokerSettingsConfig);
            return MapperMessageBrokerSettingsConfig.MapToMessageBrokerSettings(messageBrokerSettingsConfig);
        }

        public CosmosDbSettings GetCosmosDbSettings()
        {
            var cosmosDbSettingsConfig = GetCosmosDbSettingsPreValidated();
            ValidatorCosmosDbSettingsConfig.Validate(cosmosDbSettingsConfig);
            return MapperCosmosDbSettingsConfig.MapToCosmosDbSettings(cosmosDbSettingsConfig);
        }

        public StorageAccountSettings GetStorageAccountSettings()
        {
            var storageAccountSettingsConfig = GetStorageAccountSettingsPreValidated();
            ValidatorStorageAccountSettingsConfig.Validate(storageAccountSettingsConfig);
            return MapperStorageAccountSettingsConfig.MapToStorageAccountSettings(storageAccountSettingsConfig);
        }
    }
}
