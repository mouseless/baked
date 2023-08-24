using Do.Logging;
using Do.Logging.Request;

namespace Do;

public static class RequestLoggingExtensions
{
    public static ILoggingFeature RequestLogging(this LoggingConfigurator _) => new RequestLoggingFeature();
}
