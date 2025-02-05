using Baked.Architecture;
using Baked.Business;
using Baked.Domain;
using Baked.Domain.Model;
using Baked.Lifetime;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class LifetimeExtensions
{
    public static void AddLifetimes(this List<IFeature> features, IEnumerable<Func<LifetimeConfigurator, IFeature<LifetimeConfigurator>>> configures) =>
        features.AddRange(configures.Select(configure => configure(new())));

    public static void Add(this DomainServiceCollection services, TypeModel type, ServiceLifetime serviceLifetime,
        bool useFactory = true,
        bool forward = false
    ) => services.Add(
            type: type,
            serviceLifetime: serviceLifetime,
            useFactory: useFactory,
            interfaces: !type.TryGetInheritance(out var inheritance) ? [] : inheritance.Interfaces.Where(i => i.Model.TryGetMetadata(out var metadata) && metadata.Has<ServiceAttribute>()),
            forward: forward
        );
}