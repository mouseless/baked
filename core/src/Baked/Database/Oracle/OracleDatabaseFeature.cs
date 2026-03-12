using Baked.Architecture;
using Baked.Runtime;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace Baked.Database.Oracle;

public class OracleDatabaseFeature(Setting<string> _connectionString, Setting<bool> _autoUpdateSchema)
    : IFeature<DatabaseConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ITransaction, FlatTransaction>();
        });

        configurator.DataAccess.ConfigurePersistence(persistence =>
        {
            persistence.Configurer =
                OracleDataClientConfiguration.Oracle10
                    .Driver<OracleManagedDataClientDriver>()
                    .Dialect<Oracle12cDialect>()
                    .ConnectionString(_connectionString);
        });

        configurator.DataAccess.ConfigureFluentConfiguration(fluent =>
        {
            if (_autoUpdateSchema)
            {
                fluent.UpdateSchema(false, true);
            }

            fluent.ExposeConfiguration(c => c.SetProperty(NHibernate.Cfg.Environment.OracleSuppressDecimalInvalidCastException, "true"));
        });

        configurator.HttpServer.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<FlatTransactionMiddleware>();
        });
    }
}