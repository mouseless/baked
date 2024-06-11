using Baked.Logging;
using Baked.Logging.Request;

namespace Baked;

public static class RequestLoggingExtensions
{
    public static RequestLoggingFeature Request(this LoggingConfigurator _,
        bool singleLine = false
    ) => new(singleLine: singleLine);
}