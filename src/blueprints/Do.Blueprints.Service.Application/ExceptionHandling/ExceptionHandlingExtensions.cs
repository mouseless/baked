using Do.Architecture;
using Do.ExceptionHandling;

namespace Do;

public static class ExceptionHandlingExtensions
{
    public static void AddExceptionHandling(this IList<IFeature> source, Func<ExceptionHandlingConfigurator, IFeature> configure) => source.Add(configure(new()));
}
