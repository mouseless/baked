using Baked.Architecture;

namespace Mouseless.EventScheduler.Application.ConfigurationOverrider;

public class ConfigurationOverriderFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.ConfigureApiModelConventions(api =>
        {
            api.OverrideAction<DeleteMeetingContact>(routeParts: ["meeting", "contact"]);
        });
    }
}
