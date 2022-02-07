namespace DomainLayer
{
    internal static class MapperMessageBrokerSettingsConfig
    {
        public static MessageBrokerSettings MapToMessageBrokerSettings(MessageBrokerSettingsConfig monitorSettingsConfig)
        {
            return new MessageBrokerSettings(
                monitorSettingsConfig.MessageBrokerConnectionString,
                monitorSettingsConfig.MessageBrokerType
                );
        }
    }
}
