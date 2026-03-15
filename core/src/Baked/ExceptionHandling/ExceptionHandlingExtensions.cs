using Baked.Architecture;
using Baked.ExceptionHandling;

namespace Baked;

public static class ExceptionHandlingExtensions
{
    extension(IList<IFeature> features)
    {
        public void AddExceptionHandling(FeatureFunc<ExceptionHandlingConfigurator> configure) =>
            features.Add(configure(new()));
    }
}