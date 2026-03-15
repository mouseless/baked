using Baked.Architecture;
using Baked.ExceptionHandling;

namespace Baked.Playground.Override.Runtime;

public class ServicesRuntimeOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<IExceptionHandler, SampleExceptionHandler>();
            services.AddHostedService<SeedDataTrigger>();
        });
    }
}