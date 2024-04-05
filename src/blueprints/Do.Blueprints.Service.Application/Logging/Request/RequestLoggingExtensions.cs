using Do.Logging;
using Do.Logging.Request;

namespace Do;

public static class RequestLoggingExtensions
{
    public static RequestLoggingFeature RequestLogging(this LoggingConfigurator _) => new();
}