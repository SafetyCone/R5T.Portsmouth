using System;
using System.Data.SqlClient;

using R5T.Anactorium;
using R5T.Knossos;
using R5T.Magyar;

using KnossosUtilities = R5T.Knossos.Utilities;


namespace R5T.Portsmouth.Configuration
{
    public static class DatabaseConfigurationExtensions
    {
        public static ConnectionString GetConnectionString(this DatabaseConfiguration databaseConfiguration, DatabaseServerAuthentications databaseServerAuthentications)
        {
            ConnectionString connectionString;
            switch(databaseConfiguration.LocalOrRemote)
            {
                case LocalOrRemote.Local:
                    connectionString = databaseConfiguration.GetLocalConnectionString();
                    break;

                case LocalOrRemote.Remote:
                    connectionString = databaseConfiguration.GetRemoteConnectionString(databaseServerAuthentications);
                    break;

                default:
                    throw new Exception(EnumerationHelper.UnexpectedEnumerationValueMessage(databaseConfiguration.LocalOrRemote));
            }

            return connectionString;
        }

        public static ConnectionString GetRemoteConnectionString(this DatabaseConfiguration databaseConfiguration, DatabaseServerAuthentications databaseServerAuthentications)
        {
            var databaseName = databaseConfiguration.DatabaseName;

            var databaseSpecification = databaseConfiguration.DatabaseServersByDatabaseName[databaseName];

            var databaseServerSpecification = databaseConfiguration.DatabaseAuthenticationsByServerName[databaseSpecification.ServerName];

            var databaseServerLocation = databaseConfiguration.DatabaseServerLocationsByServerName[databaseSpecification.ServerName];

            var databaseServerAuthentication = databaseServerAuthentications.DatabaseServerAuthenticationsByAuthenticationName[databaseServerSpecification.AuthenticationName];

            var databaseInformation = KnossosUtilities.GetDatabaseInformation(databaseSpecification, databaseServerSpecification, databaseServerLocation, databaseServerAuthentication);

            var connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = databaseInformation.DataSource.Value,
                InitialCatalog = databaseInformation.DatabaseName.Value,
                UserID = databaseInformation.Username.Value,
                Password = databaseInformation.Password.Value,
            };

            var connectionString = new ConnectionString(connectionStringBuilder.ToString());
            return connectionString;
        }

        public static ConnectionString GetLocalConnectionString(this DatabaseConfiguration databaseConfiguration)
        {
            var databaseName = databaseConfiguration.DatabaseName;

            var localConnectionSpecification = databaseConfiguration.LocalConnectionsByDatabaseName[databaseName];

            var connectionSpecification = databaseConfiguration.ConnectionsByConnectionName[localConnectionSpecification.ConnectionName];

            var connectionString = connectionSpecification.ConnectionString;
            return connectionString;
        }
    }
}
