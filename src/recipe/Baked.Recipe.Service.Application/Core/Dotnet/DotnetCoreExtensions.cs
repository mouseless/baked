using Baked.Core;
using Baked.Core.Dotnet;

namespace Baked;

public static class DotnetCoreExtensions
{
    public static DotnetCoreFeature Dotnet(this CoreConfigurator _) =>
        new();
}