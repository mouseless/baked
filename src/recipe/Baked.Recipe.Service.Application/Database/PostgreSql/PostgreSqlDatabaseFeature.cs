using Baked.Architecture;
using Baked.Runtime.Configuration;
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
            persistence.AutoUpdateSchema = _autoUpdateSchema;
            persistence.Configurer =
                PostgreSQLConfiguration.PostgreSQL83
                    .ConnectionString(_connectionString)
                    .MaxFetchDepth(1);
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<FlatTransactionMiddleware>();
        });
    }
}