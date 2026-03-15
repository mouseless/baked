using Baked.Architecture;
using Mouseless.EventScheduler.Override.RestApi;

namespace Baked;

public static class OverrideExtensions
{
    extension(List<IFeature> features)
    {
        public void AddOverrides()
        {
            features.Add(new RoutesRestApiOverrideFeature());
        }
    }
}