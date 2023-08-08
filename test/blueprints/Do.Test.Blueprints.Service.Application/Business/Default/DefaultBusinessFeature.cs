using Do.Architecture;
using System.Reflection;

namespace Do.Test.Business.Default;

public class DefaultBusinessFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<Singleton>();

            services.AddTransientWithFactory<Entity>();
            services.AddSingleton<Entities>();

            services.AddTransientWithFactory<EntityWithGuidProperty>();
            services.AddSingleton<EntityWithGuidProperties>();
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.AddEntityAssembly(typeof(Entity).Assembly);
        });

        configurator.ConfigureApplicationParts(applicationParts =>
        {
            applicationParts.Add(new(Assembly.GetEntryAssembly()!));
        });
    }
}
