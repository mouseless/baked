using Baked.Architecture;
using Baked.DataAccess;
using Baked.Testing;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Data.Common;

using NHEnvironment = NHibernate.Cfg.Environment;

namespace Baked;

public static class DataAccessExtensions
{
    public static void AddDataAccess(this ICollection<ILayer> layers) =>
        layers.Add(new DataAccessLayer());

    public static void ConfigureFluentConfiguration(this LayerConfigurator configurator, Action<FluentConfiguration> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigurePersistence(this LayerConfigurator configurator, Action<PersistenceConfiguration> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureAutomapping(this LayerConfigurator configurator, Action<AutomappingConfiguration> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureAutoPersistenceModel(this LayerConfigurator configurator, Action<AutoPersistenceModel> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureNHibernateInterceptor(this LayerConfigurator configurator, Action<InterceptorConfiguration> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureDatabaseInitializationCollection(this LayerConfigurator configurator, Action<IDatabaseInitializationCollection> configuration) =>
        configurator.Configure((IDatabaseInitializationCollection initializations, IServiceProvider sp) => configuration(initializations));

    public static void ConfigureDatabaseInitializationCollection(this LayerConfigurator configurator, Action<IDatabaseInitializationCollection, IServiceProvider> configuration) =>
        configurator.Configure(configuration);

    public static void AddInitializer(this IDatabaseInitializationCollection initializations, Action<ISessionFactory> initializer) =>
        initializations.Add(new(initializer));

    public static void MaxFetchDepth(this FluentConfiguration configuration, int maxFetchDepth) =>
        configuration.ExposeConfiguration(c => c.SetProperty(NHEnvironment.MaxFetchDepth, $"{maxFetchDepth}"));

    public static void UpdateSchema(this FluentConfiguration configuration, bool useStdOut, bool doUpdate) =>
        configuration.ExposeConfiguration(c => new SchemaUpdate(c).Execute(useStdOut, doUpdate));

    public static void ExportSchema(this Configuration configuration, bool useStdOut, bool doUpdate, bool justDrop, DbConnection connection) =>
        new SchemaExport(configuration).Execute(useStdOut, doUpdate, justDrop, connection, null);

    public static ISession TheSession(this Stubber giveMe,
        bool clear = false
    )
    {
        var result = giveMe.The<ISession>();
        if (clear)
        {
            result.Flush();
            result.Clear();
        }

        return result;
    }
}