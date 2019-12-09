using System;

using Microsoft.Extensions.Options;

using R5T.Knossos;
using R5T.Thermopylae;

using RawDatabaseConfiguration = R5T.Portsmouth.Configuration.Raw.DatabaseConfiguration;


namespace R5T.Portsmouth.Configuration
{
    public class DatabaseConfigurationConfigureOptions : IConfigureOptions<DatabaseConfiguration>
    {
        private IOptions<RawDatabaseConfiguration> RawDatabaseConfiguration { get; }


        public DatabaseConfigurationConfigureOptions(IOptions<RawDatabaseConfiguration> rawDatabaseConfiguration)
        {
            this.RawDatabaseConfiguration = rawDatabaseConfiguration;
        }

        public void Configure(DatabaseConfiguration options)
        {
            var rawDatabaseConfiguration = this.RawDatabaseConfiguration.Value;

            options.LocalOrRemote = rawDatabaseConfiguration.LocalOrRemote;

            options.DatabaseName = new DatabaseName(rawDatabaseConfiguration.DatabaseName);

            foreach (var keyValuePair in rawDatabaseConfiguration.ConnectionStringsByConnectionName)
            {
                var connectionSpecification = new ConnectionSpecification()
                {
                    ConnectionName = new ConnectionName(keyValuePair.Key),
                    ConnectionString = new ConnectionString(keyValuePair.Value)
                };

                options.ConnectionsByConnectionName.Add(connectionSpecification.ConnectionName, connectionSpecification);
            }

            foreach (var keyValuePair in rawDatabaseConfiguration.LocalConnectionsByDatabaseName)
            {
                var localConnectionSpecification = new LocalConnectionSpecification()
                {
                    DatabaseName = new DatabaseName(keyValuePair.Key),
                    ConnectionName = new ConnectionName(keyValuePair.Value)
                };

                options.LocalConnectionsByDatabaseName.Add(localConnectionSpecification.DatabaseName, localConnectionSpecification);
            }

            foreach (var keyValuePair in rawDatabaseConfiguration.DatabaseServersByDatabaseName)
            {
                var databaseSpecification = new DatabaseSpecification()
                {
                    DatabaseName = new DatabaseName(keyValuePair.Key),
                    ServerName = new DatabaseServerName(keyValuePair.Value)
                };

                options.DatabaseServersByDatabaseName.Add(databaseSpecification.DatabaseName, databaseSpecification);
            }

            foreach (var keyValuePair in rawDatabaseConfiguration.DatabaseServerAuthenticationsByServerName)
            {
                var databaseServerSpecification = new DatabaseServerSpecification()
                {
                    ServerName = new DatabaseServerName(keyValuePair.Key),
                    AuthenticationName = new AuthenticationName(keyValuePair.Value)
                };

                options.DatabaseAuthenticationsByServerName.Add(databaseServerSpecification.ServerName, databaseServerSpecification);
            }

            foreach (var keyValuePair in rawDatabaseConfiguration.DatabaseServerLocationsByServerName)
            {
                var databaseServerLocation = new DatabaseServerLocation()
                {
                    ServerName = new DatabaseServerName(keyValuePair.Key),
                    DataSource = new DataSource(keyValuePair.Value)
                };

                options.DatabaseServerLocationsByServerName.Add(databaseServerLocation.ServerName, databaseServerLocation);
            }
        }
    }
}
