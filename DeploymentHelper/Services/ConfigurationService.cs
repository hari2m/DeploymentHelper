using System.Configuration;

namespace DeploymentHelper.Services
{
    public class ConfigurationService
    {
        public static string GetConfiguration(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}