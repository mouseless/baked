using Baked.Logging;
using Baked.Logging.Request;

namespace Baked;

public static class RequestLoggingExtensions
{
    extension(LoggingConfigurator _)
    {
        public RequestLoggingFeature Request(
            bool singleLine = false
        ) => new(singleLine: singleLine);
    }
}