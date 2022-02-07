namespace DomainLayer
{
    internal sealed class MessageBrokerSettingsConfig
    {
        public string MessageBrokerConnectionString { get; set; }

        public MessageBrokerType MessageBrokerType { get; set; }
    }
}
