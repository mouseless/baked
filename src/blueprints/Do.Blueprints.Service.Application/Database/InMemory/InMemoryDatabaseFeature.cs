using Do.Architecture;
using Do.Database.Dialects;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Database.InMemory;

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
            var sqlite = SQLiteConfiguration.Standard.InMemory().Dialect<SQLliteWithObjectDialect>();

            sqlite.MaxFetchDepth(1);

            persistence.Configurer = sqlite;
            persistence.AutoExportSchema = true;
        });
    }
}
