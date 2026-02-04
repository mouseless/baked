using Baked.Architecture;
using Baked.Business;
using Baked.Playground.CodingStyle.Id;
using Baked.Playground.Orm;

namespace Baked.Playground.Override.DataAccess;

public class MappingsDataAccessOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddPropertyAttributeConfiguration<IdAttribute>(
                when: c => c.Type.Is<EntityWithAutoIncrementId>(),
                attribute: id => id.AutoIncrement()
            );

            builder.Conventions.AddPropertyAttributeConfiguration<IdAttribute>(
                when: c => c.Type.Is<EntityWithAssignedId>(),
                attribute: id => id.Assigned()
            );
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Override<Entity>(x => x.Map(e => e.String).Length(500));
            model.Override<Entity>(x => x.Map(e => e.Unique).Column("UniqueString").Unique());
        });
    }
}