using Do.Core;
using Do.Core.Dotnet;

namespace Do;

public static class DotnetCoreExtensions
{
    public static DotnetCoreFeature Dotnet(this CoreConfigurator _) => new();
}