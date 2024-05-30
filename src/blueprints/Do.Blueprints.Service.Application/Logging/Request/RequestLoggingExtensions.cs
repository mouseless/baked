using Do.Logging;
using Do.Logging.Request;

namespace Do;

public static class RequestLoggingExtensions
{
    public static RequestLoggingFeature Request(this LoggingConfigurator _,
        bool singleLine = false
    ) => new(singleLine: singleLine);
}