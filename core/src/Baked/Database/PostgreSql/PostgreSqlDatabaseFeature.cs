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
        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ITransaction, FlatTransaction>();
        });

        configurator.DataAccess.ConfigureFluentConfiguration(fluent =>
        {
            if (_autoUpdateSchema)
            {
                fluent.UpdateSchema(false, true);
            }
        });

        configurator.DataAccess.ConfigurePersistence(persistence =>
        {
            persistence.Configurer =
                PostgreSQLConfiguration.PostgreSQL83
                    .ConnectionString(_connectionString)
                    .Dialect<CustomPostgreSQL83Dialect>()
            ;
        });

        configurator.HttpServer.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<FlatTransactionMiddleware>();
        });
    }
}