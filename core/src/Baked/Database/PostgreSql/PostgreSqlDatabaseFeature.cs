using Baked.Architecture;
using Baked.Domain.Configuration;
using Baked.Runtime;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Database.PostgreSql;

public class PostgreSqlDatabaseFeature(Setting<string> _connectionString, Setting<bool> _autoUpdateSchema)
    : IFeature<DatabaseConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
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
            if (_autoUpdateSchema)
            {
                fluent.UpdateSchema(false, true);
            }
        });

        configurator.DataAccess.ConfigurePersistence(persistence =>
        {
            persistence.Configurer =
                PostgreSQLConfiguration.PostgreSQL83
                    .ConnectionString(_connectionString)
                    .Dialect<CustomPostgreSQL83Dialect>()
            ;
        });
    }
}