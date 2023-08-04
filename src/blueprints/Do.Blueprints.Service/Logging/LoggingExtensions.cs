using Do.Architecture;
using Do.Logging;

namespace Do;

public static class LoggingExtensions
{
    public static void AddLogging(this IList<IFeature> source, Func<LoggingConfigurator, IFeature> configure) => source.Add(configure(new()));
}
