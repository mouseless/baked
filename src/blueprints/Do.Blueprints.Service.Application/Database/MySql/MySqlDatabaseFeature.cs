using Do.Architecture;
using Do.Configuration;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Database.MySql;

public class MySqlDatabaseFeature(Setting<string> _connectionString, Setting<bool> _autoUpdateSchema, Setting<bool> _showSql)
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
            var mysql = MySQLConfiguration.Standard
                .ConnectionString(_connectionString)
                .Dialect<CustomMySQL57Dialect>();

            // this should be in logging
            if (_showSql) { mysql.ShowSql(); }

            // this should be in orm
            mysql.MaxFetchDepth(1);

            persistence.Configurer = mysql;
            persistence.AutoUpdateSchema = _autoUpdateSchema;
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<FlatTransactionMiddleware>(order: -60);
        });
    }
}
