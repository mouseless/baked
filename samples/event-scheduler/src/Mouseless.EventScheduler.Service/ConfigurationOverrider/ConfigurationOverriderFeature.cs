using Do.Architecture;
using Do.Business;
using System.Reflection;

namespace Mouseless.EventScheduler;

public class ConfigurationOverriderFeature : IFeature<BusinessConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAssemblyCollection(assemblies =>
        {
            assemblies.Add(typeof(Meeting).Assembly);
        });

        configurator.ConfigureApplicationParts(applicationParts =>
        {
            applicationParts.Add(new(Assembly.GetEntryAssembly() ?? throw new NotSupportedException("Entry assembly should not be null")));
        });
    }
}
