using Baked.Architecture;
using Baked.Business;
using Baked.Lifetime;

namespace Baked.CodingStyle.RemainingServicesAreSingleton;

public class RemainingServicesAreSingletonCodingStyleFeature()
    : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetTypeAttribute(
               when: c =>
                   c.Type.IsClass && !c.Type.IsAbstract &&
                   c.Type.TryGetMembers(out var members) &&
                   members.Has<ServiceAttribute>() &&
                   !members.Has<TransientAttribute>() &&
                   !members.Has<ScopedAttribute>() &&
                   members.Properties.All(p => !p.IsPublic),
               attribute: () => new SingletonAttribute(),
               order: int.MinValue + 30
            );
        });
    }
}