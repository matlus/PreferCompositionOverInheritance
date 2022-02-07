using System;

namespace CompositionOverInheritance.Core
{

    [Serializable]
    public sealed class ConfigurationSettingMissingException : Exception
    {
        public ConfigurationSettingMissingException() { }
        public ConfigurationSettingMissingException(string message) : base(message) { }
        public ConfigurationSettingMissingException(string message, Exception inner) : base(message, inner) { }
        private ConfigurationSettingMissingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
