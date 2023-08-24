using Do.Core;
using Do.Core.Dotnet;

namespace Do;

public static class DotnetCoreExtensions
{
    public static ICoreFeature Dotnet(this CoreConfigurator _) => new DotnetCoreFeature();
}