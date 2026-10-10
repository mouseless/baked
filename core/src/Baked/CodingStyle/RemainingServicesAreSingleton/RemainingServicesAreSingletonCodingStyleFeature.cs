using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Lifetime;

namespace Baked.CodingStyle.RemainingServicesAreSingleton;

public class RemainingServicesAreSingletonCodingStyleFeature()
    : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
               attribute: () => new Singleton(),
               when: c =>
                   c.Type.IsClass && !c.Type.IsAbstract &&
                   c.Type.TryGetMembers(out var members) &&
                   members.Has<Service>() &&
                   !members.Has<Transient>() &&
                   !members.Has<Scoped>() &&
                   members.Properties.All(p => !p.IsPublic),
               order: Order.At.Max
            );
        });
    }
}