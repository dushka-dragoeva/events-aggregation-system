using Microsoft.Extensions.Configuration;

namespace EventsWebServiceTests.Configuration
{
    internal static class ConfigurationReader
    {
        internal static string GetApiKey()
        {
            return GetConfigurationValue("ApiKey");
        }

        internal static string GetApplicationUrl()
        {
            return GetConfigurationValue("ApplicationUrl");
        }

        internal static string GetConnectionString()
        {
            return GetConfigurationValue("ConnectionString");
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
