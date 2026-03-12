using Baked.ExceptionHandling;
using Baked.ExceptionHandling.ProblemDetails;
using Baked.Runtime;

namespace Baked;

public static class ProblemDetailsExceptionHandlingExtensions
{
    extension(ExceptionHandlingConfigurator _)
    {
        public ProblemDetailsExceptionHandlingFeature ProblemDetails(
            Setting<string>? typeUrlFormat = default,
            Setting<bool>? showUnhandled = default
        ) => new(typeUrlFormat, showUnhandled);
    }
}