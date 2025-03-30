using Baked.ExceptionHandling;
using Baked.ExceptionHandling.ProblemDetails;
using Baked.Runtime;

namespace Baked;

public static class ProblemDetailsExceptionHandlingExtensions
{
    public static ProblemDetailsExceptionHandlingFeature ProblemDetails(this ExceptionHandlingConfigurator _,
        Setting<string>? typeUrlFormat = default
    ) => new(typeUrlFormat);
}