using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CompositionOverInheritance.Core
{
    internal class ConfigurationProviderBase
    {
        private readonly IConfigurationRoot _configurationRoot;
        private ConfigurationSectionRetriever configurationSectionRetriever;
        protected ConfigurationSectionRetriever ConfigurationSettingRetriever { get { return configurationSectionRetriever; } set { configurationSectionRetriever = value; } }

        public ConfigurationProviderBase()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            LoadEnvironmentSpecificAppSettings(configurationBuilder);
            configurationBuilder.AddEnvironmentVariables("Alive_");

            _configurationRoot = configurationBuilder.Build();
            InitializeConfigurationSectionRetriever(_configurationRoot);            
        }

        [ExcludeFromCodeCoverage]
        internal ConfigurationProviderBase(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
            InitializeConfigurationSectionRetriever(_configurationRoot);
        }

        private void InitializeConfigurationSectionRetriever(IConfiguration configuration)
        {
            configurationSectionRetriever = new ConfigurationSectionRetriever(_configurationRoot);
        }

        private static void LoadEnvironmentSpecificAppSettings(ConfigurationBuilder configurationBuilder)
        {
            var aspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (aspNetCoreEnvironment != null)
            {
                configurationBuilder.AddJsonFile($"appsettings.{aspNetCoreEnvironment}.json");
            }
        }
    }
}
