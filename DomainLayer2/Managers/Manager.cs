using CompositionOverInheritance.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer2
{
    public class Manager
    {
        private readonly ConfigurationProvider configurationProvider;

        public Manager()
        {
            configurationProvider = new ConfigurationProvider();
        }

        public MessageBrokerSettings GetMessageBrokerSettings()
        {
            return configurationProvider.GetMessageBrokerSettings();
        }
    }
}
