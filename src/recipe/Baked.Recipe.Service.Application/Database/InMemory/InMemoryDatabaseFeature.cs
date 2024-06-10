using Baked.Architecture;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Dialect;

namespace Baked.Database.InMemory;

public class InMemoryDatabaseFeature : IFeature<DatabaseConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ITransaction, SkippedTransaction>();
        });

        configurator.ConfigurePersistence(persistence =>
        {
            var sqlite = SQLiteConfiguration.Standard.InMemory().Dialect<SQLiteDialect>();

            sqlite.MaxFetchDepth(1);

            persistence.Configurer = sqlite;
            persistence.AutoExportSchema = true;
        });
    }
}