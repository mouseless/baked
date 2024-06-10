using Do.Architecture;
using Do.DataAccess;
using FluentNHibernate.Automapping;

namespace Do;

public static class DataAccessExtensions
{
    public static void AddDataAccess(this ICollection<ILayer> layers) =>
        layers.Add(new DataAccessLayer());

    public static void ConfigurePersistence(this LayerConfigurator configurator, Action<PersistenceConfiguration> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureAutomapping(this LayerConfigurator configurator, Action<AutomappingConfiguration> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureAutoPersistenceModel(this LayerConfigurator configurator, Action<AutoPersistenceModel> configuration) =>
        configurator.Configure(configuration);

    public static void ConfigureNHibernateInterceptor(this LayerConfigurator configurator, Action<InterceptorConfiguration> configuration) =>
        configurator.Configure(configuration);
}