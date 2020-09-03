using System.Configuration;

namespace SNPIDataManager.Config
{
    public static class LocationsConfig
    {
        public static string ReadLocations(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            return appSettings[key] ?? string.Empty;
        }
    }
}