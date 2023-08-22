using Do.Architecture;
using Do.ExceptionHandling;

namespace Do;

public static class ExceptionHandlingExtensions
{
    public static void AddExceptionHandling(this IList<IFeature> source, Func<ExceptionHandlingConfigurator, IExceptionHandlingFeature> configure) => 
        source.Add((IFeature)configure(new()));
}
