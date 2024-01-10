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
            types.Add<NoServiceClass>();
            types.Add<OperationObject>();
            types.Add<Record>();
            types.Add<ServiceBase>();
            types.Add<Singleton>();
            types.Add<Status>();
            types.Add<Struct>();
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
