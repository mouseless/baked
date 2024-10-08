using Baked.Architecture;
using Baked.Runtime.Configuration;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Tool.hbm2ddl;

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

        configurator.ConfigureFluentBuilder(builder =>
        {
            builder.Database(MySQLConfiguration.Standard.ConnectionString(_connectionString).Dialect<CustomMySQL57Dialect>());

            if (_autoUpdateSchema)
            {
                builder.ExposeConfiguration(c => new SchemaUpdate(c).Execute(false, true));
            }
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<FlatTransactionMiddleware>();
        });
    }
}