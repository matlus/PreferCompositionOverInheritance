using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositionOverInheritance.Core
{
    internal sealed class MessageBrokerSettingsProvider
    {
        private readonly ConfigurationSectionRetriever configurationSectionRetriever;
        
        public MessageBrokerSettingsProvider(ConfigurationSectionRetriever configurationSectionRetriever)
        {
            this.configurationSectionRetriever = configurationSectionRetriever;
        }

        private MessageBrokerSettingsConfig GetMessageBrokerSettingsPreValidated()
        {
            const string messageBrokerSettingsKey = "MessageBroker";
            var messageBrokerSettingsConfig = configurationSectionRetriever.GetSection(messageBrokerSettingsKey).Get<MessageBrokerSettingsConfig>();

            if (messageBrokerSettingsConfig != null)
            {
                messageBrokerSettingsConfig.MessageBrokerType = messageBrokerSettingsConfig.MessageBrokerType == MessageBrokerType.None ? MessageBrokerType.ServiceBus : messageBrokerSettingsConfig.MessageBrokerType;
            }
            else
            {
                messageBrokerSettingsConfig = new MessageBrokerSettingsConfig();
            }

            return messageBrokerSettingsConfig;
        }

        public MessageBrokerSettings GetMessageBrokerSettings()
        {
            var messageBrokerSettingsConfig = GetMessageBrokerSettingsPreValidated();
            ValidatorMessageBrokerSettingsConfig.Validate(messageBrokerSettingsConfig);
            return MapperMessageBrokerSettingsConfig.MapToMessageBrokerSettings(messageBrokerSettingsConfig);
        }
    }
}
