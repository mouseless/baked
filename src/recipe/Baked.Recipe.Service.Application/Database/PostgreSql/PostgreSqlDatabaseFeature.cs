using Baked.Architecture;
using Baked.Configuration;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Database.PostgreSql;

public class PostgreSqlDatabaseFeature(Setting<string> _connectionString, Setting<bool> _autoUpdateSchema)
    : IFeature<DatabaseConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ITransaction, FlatTransaction>();
        });

        configurator.ConfigurePersistence(persistence =>
        {
            var postgresql = PostgreSQLConfiguration.PostgreSQL83
                .ConnectionString(_connectionString)
              ;

            // this should be in orm
            postgresql.MaxFetchDepth(1);

            persistence.Configurer = postgresql;
            persistence.AutoUpdateSchema = _autoUpdateSchema;
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<FlatTransactionMiddleware>();
        });
    }
}