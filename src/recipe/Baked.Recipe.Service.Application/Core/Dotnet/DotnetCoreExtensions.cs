using Baked.Core;
using Baked.Core.Dotnet;
using System.Reflection;

namespace Baked;

public static class DotnetCoreExtensions
{
    public static DotnetCoreFeature Dotnet(this CoreConfigurator _,
        Assembly? entryAssembly = default,
        string? baseNamespace = default
    ) => new(entryAssembly, baseNamespace is not null ? _ => baseNamespace : a => a.GetNameBeforeApplicationSuffix());
}