using System.Reflection;
using Do.Architecture;

namespace Do.Test.Business.Default;

public class DefaultBusinessFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<Singleton>();
        });

        configurator.ConfigureApplicationParts(applicationParts =>
        {
            applicationParts.Add(new(Assembly.GetEntryAssembly()!));
        });
    }
}
