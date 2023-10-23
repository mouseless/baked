using Do.Architecture;
using Do.Domain;
using System.Reflection;

namespace Do.Test.ConfigurationOverrider;

public class ConfigurationOverriderFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainDescriptor(descriptor =>
        {
            descriptor.AddType<Entity>();
            descriptor.AddType<Entities>();
            descriptor.AddType<Singleton>();
            descriptor.AddType<Status>();
        });

        configurator.ConfigureAutoPersistenceModel(model =>
        {
            var domainDescriptor = configurator.Context.Get<DomainDescriptor>();
            foreach (var item in domainDescriptor.AssemblyList)
            {
                model.AddEntityAssembly(item);
            }

            model.Override<Entity>(x => x.Map(e => e.String).Length(200));
        });

        configurator.ConfigureApplicationParts(applicationParts =>
        {
            applicationParts.Add(new(Assembly.GetEntryAssembly() ?? throw new NotSupportedException("Entry assembly should not be null")));
        });
    }
}
