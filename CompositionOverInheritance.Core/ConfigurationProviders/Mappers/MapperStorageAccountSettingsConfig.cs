
namespace CompositionOverInheritance.Core
{
    internal static class MapperStorageAccountSettingsConfig
    {
        public static StorageAccountSettings MapToStorageAccountSettings(StorageAccountSettingsConfig storageAccountSettingsConfig)
        {
            return new StorageAccountSettings(
                storageAccountSettingsConfig.StorageAccountConnectionString,
                storageAccountSettingsConfig.MonitorImageContainerName
                );
        }
    }
}
