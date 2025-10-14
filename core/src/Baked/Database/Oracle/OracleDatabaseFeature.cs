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
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ITransaction, FlatTransaction>();
        });

        configurator.ConfigurePersistence(persistence =>
        {
            persistence.Configurer =
                OracleDataClientConfiguration.Oracle10
                    .Driver<OracleManagedDataClientDriver>()
                    .Dialect<Oracle12cDialect>()
                    .ConnectionString(_connectionString);
        });

        configurator.ConfigureFluentConfiguration(fluent =>
        {
            if (_autoUpdateSchema)
            {
                fluent.UpdateSchema(false, true);
            }

            fluent.ExposeConfiguration(c => c.SetProperty(NHibernate.Cfg.Environment.OracleSuppressDecimalInvalidCastException, "true"));
        });

        configurator.ConfigureMiddlewareCollection(middlewares =>
        {
            middlewares.Add<FlatTransactionMiddleware>();
        });
    }
}