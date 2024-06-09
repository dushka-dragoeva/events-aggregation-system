using Microsoft.Extensions.Configuration;
using EventsWebServiceTests.Utils;

namespace EventsWebServiceTests.Configuration
{
    internal static class ConfigurationReader
    {
        internal static string GetApiKey()
        {
            return GetConfigurationValue(TestConstants.ApiKeyName);
        }

        internal static string GetApplicationUrl()
        {
            return GetConfigurationValue(TestConstants.ApplicationUrlName);
        }

        internal static string GetConnectionString()
        {
            return GetConfigurationValue(TestConstants.ConnectionStringName);
        }

        internal static string GetConfigurationValue(string name)
        {
            IConfigurationRoot configuration = GetConfiguration();

            return configuration[name];
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var dir = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            return configuration;
        }
    }
}
