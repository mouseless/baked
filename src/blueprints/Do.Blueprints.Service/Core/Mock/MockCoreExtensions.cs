
using Do.Core;
using Do.Core.Mock;

namespace Do;

public static class MockCoreExtensions
{
    public static MockCoreFeature Mock(this CoreConfigurator source) => new();
}
