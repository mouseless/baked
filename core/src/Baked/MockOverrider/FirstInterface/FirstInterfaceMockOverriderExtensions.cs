using Baked.MockOverrider;
using Baked.MockOverrider.FirstInterface;

namespace Baked;

public static class FirstInterfaceMockOverriderExtensions
{
    public static FirstInterfaceMockOverriderFeature FirstInterface(this MockOverriderConfigurator _) =>
        new();
}