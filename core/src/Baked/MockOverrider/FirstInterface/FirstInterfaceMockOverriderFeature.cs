using Baked.Architecture;
using Baked.MockOverrider;
using Baked.MockOverrider.FirstInterface;
using Microsoft.Extensions.DependencyInjection;

using FirstInterfaceMockOverrider = Baked.MockOverrider.FirstInterface.MockOverrider;

namespace Baked;

public static class FirstInterfaceMockOverriderFeature
{
    public static IFeature<MockOverriderFeature.Configurator> FirstInterface(this MockOverriderFeature.Configurator x) => x.Define(configurator =>
    {
        configurator.Testing.ConfigureTestConfiguration(test =>
        {
            test.MockFactory = new MockOverriderMockFactory();
            test.TearDowns.Add(spec =>
            {
                spec.GiveMe.The<IMockOverrider>().Reset();
            });
        });

        configurator.Runtime.ConfigureServiceCollection(services =>
        {
            services.AddSingleton<FirstInterfaceMockOverrider>();
            services.AddSingleton<IMockOverrider>(sp => sp.GetRequiredService<FirstInterfaceMockOverrider>());
        });
    });
}