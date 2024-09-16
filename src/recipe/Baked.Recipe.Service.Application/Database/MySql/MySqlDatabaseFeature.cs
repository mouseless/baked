using Baked.Architecture;
using Baked.Configuration;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Database.MySql;

public class MySqlDatabaseFeature(Setting<string> _connectionString, Setting<bool> _autoUpdateSchema)
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
                MySQLConfiguration.Standard
                    .ConnectionString(_connectionString)
                    .Dialect<CustomMySQL57Dialect>()
                    .MaxFetchDepth(1);
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<FlatTransactionMiddleware>();
        });
    }
}