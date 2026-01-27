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
                when: c => c.Type.Is<EntityWithAutoIncrementId>(),
                attribute: id => id.Orm = new(typeof(IdIntUserType)) { IdentifierGenerator = typeof(NHibernate.Id.IdentityGenerator) }
            );

            builder.Conventions.AddPropertyAttributeConfiguration<IdAttribute>(
                when: c => c.Type.Is<EntityWithAssignedId>(),
                attribute: id => id.Orm = new(typeof(IdStringUserType)) { IdentifierGenerator = typeof(NHibernate.Id.Assigned) }
            );
        });
    }
}