using Baked.Architecture;
using Baked.DataAccess;
using Baked.Testing;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using NHibernate;

using NHibEnvironment = NHibernate.Cfg.Environment;

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
      configuration.ExposeConfiguration(c => c.SetProperty(NHibEnvironment.MaxFetchDepth, $"{maxFetchDepth}"));

    public static void ShowSql(this FluentConfiguration configuration, bool showSql) =>
      configuration.ExposeConfiguration(c => c.SetProperty(NHibEnvironment.ShowSql, $"{showSql.ToString().ToLowerInvariant()}"));

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