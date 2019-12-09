using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using R5T.Sardinia;

using R5T.Portsmouth.Configuration;

using RawDatabaseConfiguration = R5T.Portsmouth.Configuration.Raw.DatabaseConfiguration;


namespace R5T.Portsmouth
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services)
        {
            services
                .Configure<RawDatabaseConfiguration>()
                .ConfigureOptions<DatabaseConfigurationConfigureOptions>()
                ;

            return services;
        }
    }
}
