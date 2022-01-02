using Microsoft.Extensions.Options;
using SmartHome.Database.Models;

namespace SmartHome.Database.Utility
{
    /// <summary>
    /// Manages the application configuration data used for initial setup.
    /// </summary>
    public class ConfigurationManager : IConfigurationManager
    {
        /// <summary>
        /// The managed application secrets.
        /// </summary>
        public AppSecrets AppSecrets { get; }
        
        /// <summary>
        /// Create a new instance of the <see cref="ConfigurationManager"/> class.
        /// </summary>
        public ConfigurationManager()
        {
            var services = GetServiceProvider();
            AppSecrets = services.GetRequiredService<IOptions<AppSecrets>>().Value;
        }

        /// <summary>
        /// Get the service provider.
        /// </summary>
        private static IServiceProvider GetServiceProvider()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsecrets.json", false, true)
                .Build();

            var provider = new ServiceCollection()
                .Configure<AppSecrets>(configuration.GetSection(nameof(AppSecrets)))
                .AddOptions()
                .BuildServiceProvider();

            return provider;
        }
    }
}
