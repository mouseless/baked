using Do.Architecture;
using Do.Business;
using System.Reflection;

namespace EventScheduler;

public class ConfigurationOverriderFeature : IFeature<BusinessConfigurator>
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureAssemblyCollection(assemblies =>
        {
            assemblies.Add(typeof(SchedulerService).Assembly);
        });

        configurator.ConfigureTypeCollection(types =>
        {
            types.Add<Meeting>();
            types.Add<Meetings>();
            types.Add<SchedulerService>();
        });

        configurator.ConfigureApplicationParts(applicationParts =>
        {
            applicationParts.Add(new(Assembly.GetEntryAssembly() ?? throw new NotSupportedException("Entry assembly should not be null")));
        });
    }
}
