using Do.Architecture;
using Do.Business;

namespace Do.Test.Business.Default;

public class DefaultBusinessFeature : IFeature<BusinessConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddTransientWithFactory<Entity>();
            services.AddSingleton<Entities>();
            services.AddSingleton<Singleton>();
        });
    }
}
