using Baked.Architecture;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.Core.Mock;

public class MockCoreFeature : IFeature<CoreConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<TimeProvider, ResettableFakeTimeProvider>();
            services.AddSingleton<FakeSettings>();
        });

        configurator.ConfigureTestConfiguration(test =>
        {
            test.Mocks.Add<IConfiguration>(singleton: true);
        });
    }
}