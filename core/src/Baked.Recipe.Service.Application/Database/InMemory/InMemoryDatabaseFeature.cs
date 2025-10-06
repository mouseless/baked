using Baked.Architecture;
using Baked.DataAccess.Sqlite;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;

namespace Baked.Database.InMemory;

public class InMemoryDatabaseFeature : IFeature<DatabaseConfigurator>
{
    IStatelessSession? _globalSession;

    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<ITransaction, SkippedTransaction>();
        });

        configurator.ConfigurePersistence(persistence =>
        {
            persistence.Configurer = SQLiteConfiguration.Microsoft.InMemory(cache: SqliteCacheMode.Shared);
        });

        configurator.ConfigureDatabaseInitializationCollection((initializations, sp) =>
        {
            initializations.AddInitializer(sf =>
            {
                // In memory db is disposed when last connection is closed, this connection is to keep the db open
                _globalSession = sf.OpenStatelessSession();

                sp.GetRequiredService<Configuration>().ExportSchema(false, true, false, _globalSession.Connection);
            });
        });

        configurator.ConfigureTestConfiguration(test =>
        {
            test.SetUps.Add(spec =>
            {
                spec.GiveMe.TheSession().BeginTransaction();
            });

            test.TearDowns.Add(spec =>
            {
                spec.GiveMe.TheSession().Flush();
                spec.GiveMe.TheSession().GetCurrentTransaction().Rollback();
                spec.GiveMe.TheSession().Clear();
            });
        });
    }
}