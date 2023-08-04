using Do.Architecture;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Do.Database.InMemory;

public class InMemoryDatabaseFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ITransaction, SkippedTransaction>();
        });

        configurator.ConfigurePersistence(persistence =>
        {
            var sqlite = SQLiteConfiguration.Standard.InMemory();

            sqlite.MaxFetchDepth(1);

            persistence.Configurer = sqlite;
            persistence.AutoExportSchema = true;
        });
    }
}
