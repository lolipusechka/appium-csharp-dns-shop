using Microsoft.Extensions.Configuration;

namespace AppiumFramework.Config
{
    public static class ConfigManager
    {
        private static IConfigurationRoot _configuration;
        private static readonly object _lock = new object();

        public static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    lock (_lock)
                    {
                        if (_configuration == null)
                        {
                            _configuration = BuildConfiguration();
                        }
                    }
                }
                return _configuration;
            }
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

        public static T GetSection<T>(string sectionName) where T : new()
        {
            var section = new T();
            Configuration.GetSection(sectionName).Bind(section);
            return section;
        }
    }
}