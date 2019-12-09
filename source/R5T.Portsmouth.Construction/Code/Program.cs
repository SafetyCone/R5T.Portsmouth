using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using R5T.Anactorium;

using R5T.Portsmouth.Configuration;

using RawDatabaseServerAuthentications = R5T.Anactorium.Raw.DatabaseServerAuthentications;
using RawDatabaseConfiguration = R5T.Portsmouth.Configuration.Raw.DatabaseConfiguration;


namespace R5T.Portsmouth.Construction
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.TryGetDatabaseConfiguration();
        }

        private static void TryGetDatabaseConfiguration()
        {
            var serviceProvider = Program.GetServiceProvider();

            var databaseConfiguration = serviceProvider.GetRequiredService<IOptions<DatabaseConfiguration>>().Value;

            Console.WriteLine($"Local or remote: {databaseConfiguration.LocalOrRemote}");

            var databaseServerAuthentications = serviceProvider.GetRequiredService<IOptions<DatabaseServerAuthentications>>().Value;

            var connectionString = databaseConfiguration.GetConnectionString(databaseServerAuthentications);

            Console.WriteLine($"Connection string: {connectionString} (Database name: {databaseConfiguration.DatabaseName})");
        }

        private static IServiceProvider GetServiceProvider()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddOptions()
                .Configure<RawDatabaseConfiguration>(configuration.GetSection(nameof(R5T.Portsmouth.Configuration.Raw.DatabaseConfiguration)))
                .ConfigureOptions<DatabaseConfigurationConfigureOptions>()
                .Configure<RawDatabaseServerAuthentications>(configuration.GetSection(nameof(R5T.Anactorium.Raw.DatabaseServerAuthentications)))
                .ConfigureOptions<DatabaseServerAuthenticationsConfigureOptions>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
