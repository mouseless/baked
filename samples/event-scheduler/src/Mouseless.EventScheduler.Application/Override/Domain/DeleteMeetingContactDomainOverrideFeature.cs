using Baked.Architecture;

namespace Mouseless.EventScheduler.Application.Override.Domain;

public class DeleteMeetingContactDomainOverrideFeature : IFeature
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
