using Do.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Do.MockOverrider.FirstInterface;

public class FirstInterfaceMockOverriderFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTestConfiguration(test =>
        {
            test.MockFactory = new MockOverriderMockFactory();
        });

        configurator.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<MockOverrider>();
            services.AddSingleton<IMockOverrider>(sp => sp.GetRequiredService<MockOverrider>());
        });
    }
}
