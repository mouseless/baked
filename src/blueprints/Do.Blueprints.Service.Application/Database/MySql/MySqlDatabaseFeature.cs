using Do.Architecture;
using Do.Configuration;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Dialect;

namespace Do.Database.MySql;

public class MySqlDatabaseFeature : IFeature<DatabaseConfigurator>
{
    readonly Setting<string> _connectionString;
    readonly Setting<bool> _autoUpdateSchema;
    readonly Setting<bool> _showSql;

    public MySqlDatabaseFeature(Setting<string> connectionString, Setting<bool> autoUpdateSchema, Setting<bool> showSql) =>
        (_connectionString, _autoUpdateSchema, _showSql) = (connectionString, autoUpdateSchema, showSql);

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
                .Dialect<DefaultMySQLDialect>();

            // this should be in logging
            if (_showSql) { mysql.ShowSql(); }

            // this should be in orm
            mysql.MaxFetchDepth(1);

            persistence.Configurer = mysql;
            persistence.AutoUpdateSchema = _autoUpdateSchema;
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<FlatTransactionMiddleware>();
        });
    }
}
