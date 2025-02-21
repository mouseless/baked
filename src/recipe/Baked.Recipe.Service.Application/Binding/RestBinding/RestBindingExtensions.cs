using Baked.Business;
using Baked.Business.DomainAssemblies;

namespace Baked;

public static class RestBindingExtensions
{
    public static RestBindingFeature RestBinding(this BusinessConfigurator _) =>
        new();
}