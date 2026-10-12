using Baked.ExceptionHandling.ProblemDetails;
using Baked.Runtime;

namespace Baked;

public static class ProblemDetailsExceptionHandlingExtensions
{
    extension(ExceptionHandlingFeature.Configurator _)
    {
        public ProblemDetailsExceptionHandlingFeature ProblemDetails(
            Setting<string>? typeUrlFormat = default,
            Setting<bool>? showUnhandled = default
        ) => new(typeUrlFormat, showUnhandled);
    }
}