using Do.Core;
using Do.Core.Mock;

namespace Do;

public static class MockCoreExtensions
{
    public static ICoreFeature Mock(this CoreConfigurator _) => new MockCoreFeature();
}
