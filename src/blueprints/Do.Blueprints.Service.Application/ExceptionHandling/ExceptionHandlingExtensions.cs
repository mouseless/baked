using Do.Architecture;
using Do.ExceptionHandling;

namespace Do;

public static class ExceptionHandlingExtensions
{
    public static void AddExceptionHandling(this IList<IFeature> source, Func<ExceptionHandlingConfigurator, IFeature<ExceptionHandlingConfigurator>> configure) =>
        source.Add(configure(new()));
}
