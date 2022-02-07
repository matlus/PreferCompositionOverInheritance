using System.Text;

namespace DomainLayer
{
    internal static class ValidatorMessageBrokerSettingsConfig
    {
        public static void Validate(MessageBrokerSettingsConfig monitorSettingsConfig)
        {
            var errorMessages = new StringBuilder();

            ValidateMessageBrokerSettingsConfig(errorMessages, monitorSettingsConfig);

            if (errorMessages.Length != 0) throw new ConfigurationSettingMissingException(errorMessages.ToString());
        }

        private static void ValidateMessageBrokerSettingsConfig(StringBuilder errorMessages, MessageBrokerSettingsConfig messageBrokerSettingsConfig)
        {
            errorMessages.AppendLineIfNotNull(ValidatorString.Validate("MessageBroker.MessageBrokerConnectionString", messageBrokerSettingsConfig.MessageBrokerConnectionString));
            errorMessages.AppendLineIfNotNull(ValidatorString.Validate("MessageBroker.MessageBrokerType", messageBrokerSettingsConfig.MessageBrokerType.ToString()));
        }
    }
}
