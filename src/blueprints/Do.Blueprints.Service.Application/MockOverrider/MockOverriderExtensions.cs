using Do.Architecture;
using Do.MockOverrider;

namespace Do;

public static partial class MockOverriderExtensions
{
    public static void AddMockOverrider(this List<IFeature> source, Func<MockOverriderConfigurator, IFeature<MockOverriderConfigurator>> configure) => source.Add(configure(new()));
}
