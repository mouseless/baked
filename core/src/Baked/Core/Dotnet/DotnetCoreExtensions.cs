using Baked.Core;
using Baked.Core.Dotnet;
using System.Reflection;

namespace Baked;

public static class DotnetCoreExtensions
{
    extension(CoreConfigurator _)
    {
        public DotnetCoreFeature Dotnet(
            Assembly? entryAssembly = default,
            string? baseNamespace = default
        ) => new(entryAssembly, baseNamespace is not null ? _ => baseNamespace : a => a.GetNameBeforeApplicationSuffix());
    }
}