using Do.Architecture;
using Do.MockOverrider;

namespace Do;

public static class MockOverriderExtensions
{
    public static void AddMockOverrider(this List<IFeature> source, Func<MockOverriderConfigurator, IMockOverriderFeature> configure) => source.Add(configure(new()));
}
