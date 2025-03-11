using Microsoft.Extensions.Configuration;

namespace API.Configuration
{
    public class DBConnections
    {
        public string ConnectionString { get; }

        public DBConnections()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            string dbServer = configuration["DB_HOST"] ?? "localhost";
            string dbPort = configuration["DB_PORT"] ?? "1433";
            string dbName = configuration["DB_NAME"] ?? "MyDatabase";
            string dbUser = configuration["DB_USER"] ?? "sa";
            string dbPassword = configuration["DB_PASS"] ?? "YourStrong!Passw0rd";

            ConnectionString = $"Server={dbServer},{dbPort};Database={dbName};User Id={dbUser};Password={dbPassword};TrustServerCertificate=True;";
        }
    }
}
