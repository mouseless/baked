using Baked.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.MockOverrider.FirstInterface;

public class FirstInterfaceMockOverriderFeature : IFeature<MockOverriderConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTestConfiguration(test =>
        {
            test.MockFactory = new MockOverriderMockFactory();
            test.TearDowns.Add(spec =>
            {
                spec.GiveMe.The<IMockOverrider>().Reset();
            });
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<MockOverrider>();
            services.AddSingleton<IMockOverrider>(sp => sp.GetRequiredService<MockOverrider>());
        });
    }
}