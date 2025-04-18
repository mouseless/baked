using Baked.ExceptionHandling.ProblemDetails;
using Baked.Runtime;
using Baked.Testing;

namespace Baked.Test;

public static class ExceptionHandlerExtensions
{
    public static UnhandledExceptionHandler AnUnhandledExceptionHandler(this Stubber giveMe,
        bool? showUnhandled = default
    ) => new(giveMe.AnExceptionHandlerSettings(showUnhandled: showUnhandled));

    public static ClientExceptionHandler AClientExceptionHandler(this Stubber giveMe,
        bool? showUnhandled = false
      ) => new(giveMe.AnExceptionHandlerSettings(showUnhandled: showUnhandled));

    public static ExceptionHandlerSettings AnExceptionHandlerSettings(this Stubber _,
        string? typeUrlFormat = default,
        bool? showUnhandled = default
    )
    {
        showUnhandled ??= false;

        return new(
            TypeUrlFormat: typeUrlFormat is null ? (Setting<string>?)null : typeUrlFormat,
            ShowUnhandled: showUnhandled
        );
    }
}