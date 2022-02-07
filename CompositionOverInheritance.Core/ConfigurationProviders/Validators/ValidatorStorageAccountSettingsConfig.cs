using System.Text;

namespace CompositionOverInheritance.Core
{
    internal static class ValidatorStorageAccountSettingsConfig
    {
        public static void Validate(StorageAccountSettingsConfig storageAccountSettingsConfig)
        {
            var errorMessages = new StringBuilder();

            ValidateStorageAccountSettingsConfig(errorMessages, storageAccountSettingsConfig);

            if (errorMessages.Length != 0) throw new ConfigurationSettingMissingException(errorMessages.ToString());
        }

        private static void ValidateStorageAccountSettingsConfig(StringBuilder errorMessages, StorageAccountSettingsConfig storageAccountSettingsConfig)
        {
            errorMessages.AppendLineIfNotNull(ValidatorString.Validate("StorageAccount.StorageAccountConnectionString", storageAccountSettingsConfig.StorageAccountConnectionString));            
        }
    }
}
