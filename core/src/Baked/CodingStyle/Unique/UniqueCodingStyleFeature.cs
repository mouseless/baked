using Baked.Architecture;
using Baked.Business;
using Baked.Orm;

namespace Baked.CodingStyle.Unique;

public class UniqueCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.SetPropertyAttribute(
                when: c =>
                    c.Type.Has<EntityAttribute>() &&
                    c.Type.TryGet<LocatableAttribute>(out var locatable) &&
                    locatable.QueryType is not null &&
                    c.Domain.Types[locatable.QueryType].TryGetMembers(out var query) &&
                    (
                        query.Methods.Contains($"SingleBy{c.Property.Name}") ||
                        query.Methods.Contains($"AnyBy{c.Property.Name}")
                    ),
                attribute: c => new UniqueAttribute(),
                order: 30
            );
        });
    }
}