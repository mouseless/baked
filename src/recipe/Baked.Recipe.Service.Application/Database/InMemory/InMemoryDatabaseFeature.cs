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

        configurator.ConfigureFluentBuilder(builder =>
        {
            builder.Database(SQLiteConfiguration.Microsoft.InMemory());
        });

        configurator.ConfigurePersistence(persistence =>
        {
            persistence.AutoExportSchema = true;
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