using Baked.Core;
using Baked.Core.Mock;

namespace Baked;

public static class MockCoreExtensions
{
    public static MockCoreFeature Mock(this CoreConfigurator _) =>
        new();
}