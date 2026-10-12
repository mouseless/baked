using Baked.Logging.Request;

namespace Baked;

public static class RequestLoggingExtensions
{
    extension(LoggingFeature.Configurator _)
    {
        public RequestLoggingFeature Request(
            bool singleLine = false
        ) => new(singleLine: singleLine);
    }
}