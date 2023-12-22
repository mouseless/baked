using Do.Architecture;
using System.Reflection;

namespace Do.Test.ConfigurationOverrider;

public class ConfigurationOverriderFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAssemblyCollection(assemblies =>
        {
            assemblies.Add(typeof(Build).Assembly);
        });

        configurator.ConfigureTypeCollection(types =>
        {
            foreach (var item in typeof(Build).Assembly.GetTypes())
            {
                types.Add(item);
            }
        });

        configurator.ConfigureApplicationParts(applicationParts =>
        {
            applicationParts.Add(new(Assembly.GetEntryAssembly() ?? throw new NotSupportedException("Entry assembly should not be null")));
        });
    }
}
