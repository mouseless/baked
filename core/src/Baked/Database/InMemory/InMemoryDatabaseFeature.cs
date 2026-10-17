using Baked.Architecture;
using Baked.DataAccess.Sqlite;
using Baked.Database.InMemory;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;

using ITransaction = Baked.Database.ITransaction;

namespace Baked;

public static class InMemoryDatabaseFeature
{
    public static IFeature<DatabaseFeature.Configurator> InMemory(this DatabaseFeature.Configurator x)
    {
        IStatelessSession? globalSession = null;

        return x.Define(configurator =>
        {
            configurator.Runtime.ConfigureServiceCollection(services =>
            {
                services.AddSingleton<ITransaction, SkippedTransaction>();
            });

            configurator.DataAccess.ConfigurePersistence(persistence =>
            {
                persistence.Configurer = SQLiteConfiguration.Microsoft.InMemory(cache: SqliteCacheMode.Shared);
            });

            configurator.DataAccess.ConfigureDatabaseInitializationCollection((initializations, sp) =>
            {
                initializations.AddInitializer(sf =>
                {
                    // In memory db is disposed when last connection is closed, this connection is to keep the db open
                    globalSession = sf.OpenStatelessSession();

                    sp.GetRequiredService<Configuration>().ExportSchema(false, true, false, globalSession.Connection);
                });
            });

            configurator.Testing.ConfigureTestConfiguration(test =>
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
        });
    }
}