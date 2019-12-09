using System;

using Microsoft.Extensions.Options;

using R5T.Anactorium;
using R5T.Lincoln;

using R5T.Portsmouth.Configuration;


namespace R5T.Portsmouth
{
    public class StandardConnectionStringProvider : IConnectionStringProvider
    {
        private IOptions<DatabaseConfiguration> DatabaseConfiguration { get; }
        private IOptions<DatabaseServerAuthentications> DatabaseServerAuthentications { get; }


        public StandardConnectionStringProvider(IOptions<DatabaseConfiguration> databaseConfiguration, IOptions<DatabaseServerAuthentications> databaseServerAuthentications)
        {
            this.DatabaseConfiguration = databaseConfiguration;
            this.DatabaseServerAuthentications = databaseServerAuthentications;
        }

        public string GetConnectionString()
        {
            var databaseConfiguration = this.DatabaseConfiguration.Value;
            var databaseServerAuthentications = this.DatabaseServerAuthentications.Value;

            var connectionString = databaseConfiguration.GetConnectionString(databaseServerAuthentications);

            var output = connectionString.Value;
            return output;
        }
    }
}
