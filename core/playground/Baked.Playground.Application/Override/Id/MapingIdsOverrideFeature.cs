using Baked.Architecture;
using Baked.Business;
using Baked.CodingStyle.Id;
using Baked.Playground.CodingStyle;

namespace Baked.Playground.Override.Id;

public class MapingIdsOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddPropertyAttributeConfiguration<IdAttribute>(
                when: c => c.Type.Is<EntityWithIntId>(),
                attribute: id => id.Orm = new(typeof(IdIntUserType)) { IdentifierGenerator = typeof(IdIntGenerator) }
            );

            builder.Conventions.AddPropertyAttributeConfiguration<IdAttribute>(
                when: c => c.Type.Is<EntityWithAssignedId>(),
                attribute: id => id.Orm = new(typeof(IdAssignedUserType)) { IdentifierGenerator = typeof(IdAssignedGenerator) }
            );
        });

        //configurator.ConfigureAutoPersistenceModel(model =>
        //{
        //    model.Override<EntityWithIntId>(x => x.Id(e => e.Id).GeneratedBy.Identity());
        //});
    }
}