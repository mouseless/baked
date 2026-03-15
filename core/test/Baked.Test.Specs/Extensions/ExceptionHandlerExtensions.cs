using Baked.ExceptionHandling.ProblemDetails;
using Baked.Runtime;
using Baked.Testing;

namespace Baked.Test;

public static class ExceptionHandlerExtensions
{
    extension(Stubber giveMe)
    {
        public UnhandledExceptionHandler AnUnhandledExceptionHandler(
            bool? showUnhandled = default
        ) => new(giveMe.AnExceptionHandlerSettings(showUnhandled: showUnhandled));

        public ClientExceptionHandler AClientExceptionHandler(
            bool? showUnhandled = false
        ) => new(giveMe.AnExceptionHandlerSettings(showUnhandled: showUnhandled));
    }

    extension(Stubber _)
    {
        public ExceptionHandlerSettings AnExceptionHandlerSettings(
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
}