using System;

using Microsoft.Extensions.Options;

using R5T.Anactorium;
using R5T.Lincoln;

using R5T.Portsmouth.Configuration;using R5T.T0064;


namespace R5T.Portsmouth
{[ServiceImplementationMarker]
    public class StandardConnectionStringProvider : IConnectionStringProvider,IServiceImplementation
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
