using Baked.ExceptionHandling;
using Baked.ExceptionHandling.ProblemDetails;
using Baked.Runtime;
using Baked.Theme.Admin;

namespace Baked;

public static class ProblemDetailsExceptionHandlingExtensions
{
    public static ProblemDetailsExceptionHandlingFeature ProblemDetails(this ExceptionHandlingConfigurator _,
        Setting<string>? typeUrlFormat = default,
        List<ErrorHandlingPlugin.Handler>? handlers = default
    ) => new(typeUrlFormat, handlers);
}