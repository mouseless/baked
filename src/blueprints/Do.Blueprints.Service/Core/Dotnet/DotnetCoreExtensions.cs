using Do.Blueprints.Service.Core;
using Do.Blueprints.Service.Core.Dotnet;

namespace Do;

public static class DotnetCoreExtensions
{
    public static DotnetCoreFeature Dotnet(this CoreConfigurator source) => new();
}