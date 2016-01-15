using Microsoft.Extensions.Configuration;

namespace DerAlbert.Configuration
{
    public interface IConnectionManager
    {
        string GetConnectionString(string name);
    }

    public class ConnectionManager : IConnectionManager
    {
        private readonly IConfigurationSection configuration;

        public ConnectionManager(IConfigurationRoot configuration)
        {
            this.configuration = configuration.GetSection("connectionStrings");

        }

        public string GetConnectionString(string name)
        {
            return configuration[name];
        }
    }
}