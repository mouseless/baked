using Baked.Architecture;
using Baked.Test.Orm;

namespace Baked.Test.Override.DataAccess;

public class MappingsDataAccessOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Override<Entity>(x => x.Map(e => e.String).Length(500));
            model.Override<Entity>(x => x.Map(e => e.Unique).Column("UniqueString").Unique());
        });
    }
}