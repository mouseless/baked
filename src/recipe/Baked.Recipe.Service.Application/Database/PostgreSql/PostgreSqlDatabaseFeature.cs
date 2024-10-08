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

        configurator.ConfigureFluentBuilder(builder =>
        {
            builder.Database(PostgreSQLConfiguration.PostgreSQL83.ConnectionString(_connectionString));

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