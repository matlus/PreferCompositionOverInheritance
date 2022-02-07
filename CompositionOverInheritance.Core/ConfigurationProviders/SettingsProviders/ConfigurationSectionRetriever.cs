using Microsoft.Extensions.Configuration;

namespace CompositionOverInheritance.Core
{
    internal sealed class ConfigurationSectionRetriever
    {
        private readonly IConfiguration configuration;

        public ConfigurationSectionRetriever(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IConfigurationSection GetSection(string key)
        {
            return configuration.GetSection(key);
        }

        private string RetrieveConfigurationSettingValue(string key)
        {
            return configuration[key];
        }

        public string RetrieveConfigurationSettingValueOrNull(string key)
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
    }
}
