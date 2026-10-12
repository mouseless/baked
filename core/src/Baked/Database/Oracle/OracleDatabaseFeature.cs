using Baked.Architecture;
using Baked.Database;
using Baked.Domain.Configuration;
using Baked.Runtime;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace Baked;

public static class OracleDatabaseFeature
{
    public static IFeature<DatabaseFeature.Configurator> Oracle(this DatabaseFeature.Configurator x,
        Setting<string>? connectionString = default,
        Setting<bool>? autoUpdateSchema = default
    ) => x.Define((
        connectionString ?? Settings.Required<string>("Database:Oracle:ConnectionString"),
        autoUpdateSchema ?? Settings.Optional("Database:Oracle:AutoUpdateSchema", false)
    ), (configurator, args) =>
    {
        var (connectionString, autoUpdateSchema) = args;

        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.Add(new AddTransactionFilterToActionConvention(), order: Order.At.Global.Max);
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ITransaction, FlatTransaction>();
            services.AddScoped<TransactionFilter>();
        });

        configurator.DataAccess.ConfigurePersistence(persistence =>
        {
            persistence.Configurer =
                OracleDataClientConfiguration.Oracle10
                    .Driver<OracleManagedDataClientDriver>()
                    .Dialect<Oracle12cDialect>()
                    .ConnectionString(connectionString);
        });

        configurator.DataAccess.ConfigureFluentConfiguration(fluent =>
        {
            if (autoUpdateSchema)
            {
                fluent.UpdateSchema(false, true);
            }

            fluent.ExposeConfiguration(c => c.SetProperty(NHibernate.Cfg.Environment.OracleSuppressDecimalInvalidCastException, "true"));
        });
    });
}