using System;
using System.Collections.Generic;

using R5T.Knossos;


namespace R5T.Portsmouth.Configuration.Raw
{
    public class DatabaseConfiguration
    {
        public LocalOrRemote LocalOrRemote { get; set; }
        public string DatabaseName { get; set; }
        public Dictionary<string, string> ConnectionStringsByConnectionName { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> LocalConnectionsByDatabaseName { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> DatabaseServersByDatabaseName { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> DatabaseServerAuthenticationsByServerName { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> DatabaseServerLocationsByServerName { get; set; } = new Dictionary<string, string>();
    }
}
