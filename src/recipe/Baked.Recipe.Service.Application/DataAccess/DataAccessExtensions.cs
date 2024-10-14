using Baked.Architecture;
using Baked.DataAccess;
using Baked.Testing;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

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

    public static void MaxFetchDepth(this FluentConfiguration configuration, int maxFetchDepth) =>
      configuration.ExposeConfiguration(c => c.SetProperty(NHEnvironment.MaxFetchDepth, $"{maxFetchDepth}"));

    public static void UpdateSchema(this FluentConfiguration configuration, bool useStdOut, bool doUpdate) =>
        configuration.ExposeConfiguration(c => new SchemaUpdate(c).Execute(useStdOut, doUpdate));

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