using Baked.Architecture;
using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Orm;

namespace Baked.CodingStyle.UniqueViaSingleBy;

public class UniqueViaSingleByCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetPropertyAttribute(
                when: c =>
                    c.Type.Has<Entity>() &&
                    c.Type.TryGet<Locatable>(out var locatable) &&
                    locatable.QueryType is not null &&
                    c.Domain.Types[locatable.QueryType].TryGetMembers(out var query) &&
                    query.Methods.Contains($"SingleBy{c.Property.Name}"),
                attribute: c => new Unique(),
                order: Order.At.Infra + 30
            );
        });
    }
}