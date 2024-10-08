using Baked.Architecture;
using Baked.Runtime.Configuration;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Tool.hbm2ddl;

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
                fluent.ExposeConfiguration(c => new SchemaUpdate(c).Execute(false, true));
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