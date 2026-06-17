using Baked.Architecture;

namespace Mouseless.EventScheduler.Override.RestApi;

public class RoutesRestApiOverrideFeature : IFeature
{
    public void Configure(LayerConfigurator configurator)
    {
        configurator.Domain.ConfigureConventions(conventions =>
        {
            conventions.AddOverrideAction<DeleteMeetingContact>(nameof(DeleteMeetingContact.Execute),
                routeParts: ["meeting", "contact"]
            );
        });
    }
}