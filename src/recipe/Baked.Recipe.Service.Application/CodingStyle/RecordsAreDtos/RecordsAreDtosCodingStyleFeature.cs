using Baked.Architecture;
using Baked.Business;

namespace Baked.CodingStyle.RecordsAreDtos;

public class RecordsAreDtosCodingStyleFeature : IFeature<CodingStyleConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddTypeMetadata(new ApiInputAttribute(),
                when: c =>
                    c.Type.TryGetMembers(out var members) &&
                    members.Methods.Contains("<Clone>$") // if type is record
            );
        });
    }
}