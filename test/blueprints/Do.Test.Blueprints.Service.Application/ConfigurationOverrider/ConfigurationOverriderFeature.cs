using Do.Architecture;
using System.Reflection;

namespace Do.Test.ConfigurationOverrider;

public class ConfigurationOverriderFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAssemblyCollection(assemblies =>
        {
            assemblies.Add(typeof(Entity).Assembly);
        });

        configurator.ConfigureTypeCollection(types =>
        {
            types.Add<Entity>();
            types.Add<Entities>();
            types.Add<Singleton>();
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            model.Override<Entity>(x => x.Map(e => e.String).Length(500));
        });

        configurator.ConfigureApplicationParts(applicationParts =>
        {
            applicationParts.Add(new(Assembly.GetEntryAssembly() ?? throw new NotSupportedException("Entry assembly should not be null")));
        });
    }
}
