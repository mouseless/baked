using Baked.Architecture;
using Baked.DataAccess.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

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

        configurator.ConfigureTestConfiguration(test =>
        {
            test.SetUps.Add(spec =>
            {
                spec.GiveMe.The<ISession>().BeginTransaction();
            });
            test.TearDowns.Add(spec =>
            {
                spec.GiveMe.The<ISession>().Flush();
                spec.GiveMe.The<ISession>().GetCurrentTransaction().Rollback();
                spec.GiveMe.The<ISession>().Clear();
            });
        });

    }
}