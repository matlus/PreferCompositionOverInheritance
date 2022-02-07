namespace DomainLayer
{
    public sealed record MessageBrokerSettings(string MessageBrokerConnectionString, MessageBrokerType MessageBrokerType);
}
