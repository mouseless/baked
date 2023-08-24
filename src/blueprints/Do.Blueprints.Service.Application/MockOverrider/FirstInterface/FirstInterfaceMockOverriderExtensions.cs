using Do.MockOverrider;
using Do.MockOverrider.FirstInterface;

namespace Do;

public static class FirstInterfaceMockOverriderExtensions
{
    public static IMockOverriderFeature FirstInterface(this MockOverriderConfigurator _) => new FirstInterfaceMockOverriderFeature();
}
