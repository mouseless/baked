using Do.Architecture;

namespace Do.Test.ConfigurationOverrider;

public class ConfigurationOverriderFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureTypeCollection(types => types.Add<OperationWithGenericParameter<Entity>>());

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Override<Entity>(x => x.Map(e => e.String).Length(500));
            model.Override<Entity>(x => x.Map(e => e.Uniq).Unique());
        });
    }
}
