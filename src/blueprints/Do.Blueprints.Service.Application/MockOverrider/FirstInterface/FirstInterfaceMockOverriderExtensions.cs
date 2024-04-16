using Do.MockOverrider;
using Do.MockOverrider.FirstInterface;

namespace Do;

public static class FirstInterfaceMockOverriderExtensions
{
    public static FirstInterfaceMockOverriderFeature FirstInterface(this MockOverriderConfigurator _) =>
        new();
}