using Baked.Architecture;
using Mouseless.EventScheduler.Override.RestApi;

namespace Baked;

public static class OverrideExtensions
{
    public static void AddOverrides(this List<IFeature> features)
    {
        features.Add(new RoutesRestApiOverrideFeature());
    }
}