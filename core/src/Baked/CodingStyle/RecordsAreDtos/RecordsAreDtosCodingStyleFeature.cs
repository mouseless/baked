using Baked.Architecture;
using Baked.Domain.Configuration;
using Baked.RestApi.Model;

namespace Baked.CodingStyle.RecordsAreDtos;

public class RecordsAreDtosCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.SetTypeAttribute(
                attribute: () => new ApiInputAttribute(),
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Methods.Contains("<Clone>$"), // if type is record
                order: Order.At.Infra
            );
        });
    }
}