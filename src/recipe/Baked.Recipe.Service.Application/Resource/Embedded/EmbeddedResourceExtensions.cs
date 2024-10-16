using Baked.Resource;
using Baked.Resource.Embedded;
using System.Reflection;

namespace Baked;

public static class EmbeddedResourceExtensions
{
    public static EmbeddedResourceFeature EmbeddedResource(this ResourceConfigurator _, List<(Assembly assembly, string? baseNameSpace)> assemblies) =>
        new(assemblies);
}