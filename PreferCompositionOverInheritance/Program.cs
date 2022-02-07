using System;

namespace PreferCompositionOverInheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var manager = new DomainLayer2.Manager();
            var messageBrokerSettings = manager.GetMessageBrokerSettings();
        }
    }
}
