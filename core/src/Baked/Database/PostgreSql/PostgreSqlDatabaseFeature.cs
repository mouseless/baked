using Baked.Architecture;
using Baked.Database;
using Baked.Database.PostgreSql;
using Baked.Domain.Configuration;
using Baked.Runtime;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class PostgreSqlDatabaseFeature
{
    public static IFeature<DatabaseFeature.Configurator> PostgreSql(this DatabaseFeature.Configurator x,
        Setting<string>? connectionString = default,
        Setting<bool>? autoUpdateSchema = default
    ) => x.Define(configurator =>
    {
        connectionString ??= Settings.Required<string>("Database:PostgreSql:ConnectionString");
        autoUpdateSchema ??= Settings.Optional("Database:PostgreSql:AutoUpdateSchema", false);

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
                PostgreSQLConfiguration.PostgreSQL83
                    .ConnectionString(connectionString)
                    .Dialect<CustomPostgreSQL83Dialect>()
            ;
        });
    });
}