using Baked.Architecture;
using Mouseless.EventScheduler.Application.Override.Domain;

namespace Baked;

public static class OverrideExtensions
{
    public static void AddOverrides(this List<IFeature> features)
    {
        features.Add(new DeleteMeetingContactDomainOverrideFeature());
    }
}
