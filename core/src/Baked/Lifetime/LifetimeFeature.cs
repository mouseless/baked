using Baked.Architecture;
using Baked.Business;
using Baked.Domain;
using Baked.Domain.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Baked;

public static class LifetimeFeature
{
    public static void AddLifetimes(this List<IFeature> features,
        IEnumerable<FeatureFunc<Configurator>> configures
    ) => features.AddRange(configures.Select(configure => configure(new())));

    public class Configurator : FeatureConfigurator<Configurator>;

    extension(DomainServiceCollection services)
    {
        public void Add(TypeModel type, ServiceLifetime serviceLifetime,
            bool useFactory = true,
            bool forward = false
        ) => services.Add(
            type: type,
            serviceLifetime: serviceLifetime,
            useFactory: useFactory,
            interfaces: !type.TryGetInheritance(out var inheritance) ? [] : inheritance.Interfaces.Where(i => i.Model.TryGetMetadata(out var metadata) && metadata.Has<Service>()),
            forward: forward
        );
    }
}