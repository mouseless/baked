using Baked.Architecture;
using Baked.ExceptionHandling;

namespace Baked;

public static class ExceptionHandlingExtensions
{
    public static void AddExceptionHandling(this IList<IFeature> features, Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>> configure) =>
        features.Add(configure(new()));
}