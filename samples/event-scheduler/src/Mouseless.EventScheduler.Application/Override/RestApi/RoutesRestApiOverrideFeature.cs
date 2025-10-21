using Baked.Architecture;

namespace Mouseless.EventScheduler.Override.RestApi;

public class RoutesRestApiOverrideFeature : IFeature
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