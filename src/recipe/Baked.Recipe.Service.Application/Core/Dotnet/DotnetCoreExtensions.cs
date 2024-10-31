using Baked.Core;
using Baked.Core.Dotnet;
using System.Reflection;

namespace Baked;

public static class DotnetCoreExtensions
{
    public static DotnetCoreFeature Dotnet(this CoreConfigurator _,
        Assembly? entryAssembly = default,
        Func<Assembly, string?>? baseNamespace = default
    )
    {
        baseNamespace ??= assembly => Regexes.AssemblyNameBeforeApplicationSuffix().Match(assembly.FullName ?? string.Empty).Value;

        return new(entryAssembly, baseNamespace);
    }
}