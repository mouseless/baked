using Baked.Architecture;
using Baked.Runtime;
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

        configurator.ConfigureFluentConfiguration(fluent =>
        {
            if (_autoUpdateSchema)
            {
                fluent.UpdateSchema(false, true);
            }
        });

        configurator.ConfigurePersistence(persistence =>
        {
            persistence.Configurer = PostgreSQLConfiguration.PostgreSQL83.ConnectionString(_connectionString);
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<FlatTransactionMiddleware>();
        });
    }
}