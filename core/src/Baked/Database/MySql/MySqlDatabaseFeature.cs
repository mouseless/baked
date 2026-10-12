using Baked.Architecture;
using Baked.Database;
using Baked.Database.MySql;
using Baked.Domain.Configuration;
using Baked.Runtime;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class MySqlDatabaseFeature
{
    public static IFeature<DatabaseFeature.Configurator> MySql(this DatabaseFeature.Configurator x,
        Setting<string>? connectionString = default,
        Setting<bool>? autoUpdateSchema = default
    ) => x.Define((
        connectionString ?? Settings.Required<string>("Database:MySql:ConnectionString"),
        autoUpdateSchema ?? Settings.Optional("Database:MySql:AutoUpdateSchema", false)
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

        configurator.DataAccess.ConfigureFluentConfiguration(fluent =>
        {
            if (autoUpdateSchema)
            {
                fluent.UpdateSchema(false, true);
            }
        });

        configurator.DataAccess.ConfigurePersistence(persistence =>
        {
            persistence.Configurer =
                MySQLConfiguration.Standard
                    .ConnectionString(connectionString)
                    .Dialect<CustomMySQL57Dialect>();
        });
    });
}