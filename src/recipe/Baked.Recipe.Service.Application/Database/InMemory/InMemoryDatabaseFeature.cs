using Baked.Architecture;
using Baked.DataAccess.Sqlite;
using Microsoft.Extensions.DependencyInjection;

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
            persistence.AutoExportSchema = true;
            persistence.Configurer =
                SQLiteConfiguration.Microsoft
                    .InMemory()
                    .MaxFetchDepth(1);
        });
    }
}