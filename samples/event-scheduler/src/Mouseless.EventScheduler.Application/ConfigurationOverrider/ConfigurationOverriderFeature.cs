using Baked.Architecture;

namespace Mouseless.EventScheduler.Application.ConfigurationOverrider;

public class ConfigurationOverriderFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureDomainModelBuilder(builder =>
        {
            builder.Conventions.AddOverrideAction<DeleteMeetingContact>(nameof(DeleteMeetingContact.Execute),
                routeParts: ["meeting", "contact"]
            );
        });
    }
}
