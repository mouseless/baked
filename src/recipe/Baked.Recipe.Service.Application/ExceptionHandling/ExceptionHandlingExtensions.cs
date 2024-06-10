using Baked.Architecture;
using Baked.ExceptionHandling;

namespace Baked;

public static class ExceptionHandlingExtensions
{
    public static void AddExceptionHandling(this IList<IFeature> source, Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>> configure) =>
        source.Add(configure(new()));
}