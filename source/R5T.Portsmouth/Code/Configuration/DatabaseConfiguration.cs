using System;
using System.Collections.Generic;

using R5T.Knossos;


namespace R5T.Portsmouth.Configuration
{
    public class DatabaseConfiguration
    {
        public LocalOrRemote LocalOrRemote { get; set; }
        public DatabaseName DatabaseName { get; set; }
        public Dictionary<ConnectionName, ConnectionSpecification> ConnectionsByConnectionName { get; set; } = new Dictionary<ConnectionName, ConnectionSpecification>();
        public Dictionary<DatabaseName, LocalConnectionSpecification> LocalConnectionsByDatabaseName { get; set; } = new Dictionary<DatabaseName, LocalConnectionSpecification>();
        public Dictionary<DatabaseName, DatabaseSpecification> DatabaseServersByDatabaseName { get; set; } = new Dictionary<DatabaseName, DatabaseSpecification>();
        public Dictionary<DatabaseServerName, DatabaseServerSpecification> DatabaseAuthenticationsByServerName { get; set; } = new Dictionary<DatabaseServerName, DatabaseServerSpecification>();
        public Dictionary<DatabaseServerName, DatabaseServerLocation> DatabaseServerLocationsByServerName { get; set; } = new Dictionary<DatabaseServerName, DatabaseServerLocation>();
    }
}
