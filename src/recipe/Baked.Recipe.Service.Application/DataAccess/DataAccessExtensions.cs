using Baked.Architecture;
using Baked.DataAccess;
using Baked.Testing;
using FluentNHibernate.Automapping;
using NHibernate;

namespace Baked;

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

    public static void AClearSession(this Stubber giveMe)
    {
        giveMe.The<ISession>().Flush();
        giveMe.The<ISession>().Clear();
    }
}