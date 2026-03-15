using Baked.MockOverrider;
using Baked.MockOverrider.FirstInterface;

namespace Baked;

public static class FirstInterfaceMockOverriderExtensions
{
    extension(MockOverriderConfigurator _)
    {
        public FirstInterfaceMockOverriderFeature FirstInterface() =>
            new();
    }
}