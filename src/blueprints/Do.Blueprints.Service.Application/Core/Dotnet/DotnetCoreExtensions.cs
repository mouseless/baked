using Do.Core;
using Do.Core.Dotnet;
using Do.Testing;
using Shouldly;
using System.Reflection;

namespace Do;

public static class DotnetCoreExtensions
{
    public static DotnetCoreFeature Dotnet(this CoreConfigurator _) => new();
}
