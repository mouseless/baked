using Do.Architecture;
using Do.Configuration;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Dialect;

namespace Do.Database.MySql;

public class MySqlDatabaseFeature : IFeature
{
    readonly Setting<string> _connectionString;
    readonly Setting<bool> _autoUpdateSchema;
    readonly Setting<bool> _showSql;

    public MySqlDatabaseFeature(Setting<string> connectionString, Setting<bool> autoUpdateSchema, Setting<bool> showSql) =>
        (_connectionString, _autoUpdateSchema, _showSql) = (connectionString, autoUpdateSchema, showSql);

    bool Disabled => _connectionString == string.Empty;

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            if (Disabled) { return; }

            services.AddSingleton<ITransaction, FlatTransaction>();
        });

        configurator.ConfigurePersistence(persistence =>
        {
            if (Disabled) { return; }

            var mysql = MySQLConfiguration.Standard
                .ConnectionString(_connectionString)
                .Dialect<MySQL57Dialect>();

            // this should be in logging
            if (_showSql) { mysql.ShowSql(); }

            // this should be in orm
            mysql.MaxFetchDepth(1);

            persistence.Configurer = mysql;
            persistence.AutoUpdateSchema = _autoUpdateSchema;
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            if (Disabled) { return; }

            middlewares.Add<FlatTransactionMiddleware>();
        });
    }
}
