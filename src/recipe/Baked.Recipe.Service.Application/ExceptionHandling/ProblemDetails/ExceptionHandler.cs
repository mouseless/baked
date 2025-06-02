using Baked.Localization;
using Humanizer;
using Microsoft.AspNetCore.Http;

namespace Baked.ExceptionHandling.ProblemDetails;

public class ExceptionHandler(IEnumerable<IExceptionHandler> _handlers, ExceptionHandlerSettings _settings, ILocalizer _localizer)
    : Microsoft.AspNetCore.Diagnostics.IExceptionHandler
{
    readonly UnhandledExceptionHandler _unhandledExceptionHandler = new(_settings);

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionInfo = HandlerFor(exception).Handle(exception);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = exceptionInfo.Code;

        await httpContext.Response.WriteAsJsonAsync(ToProblemDetails(exceptionInfo), cancellationToken);

        return true;
    }

    IExceptionHandler HandlerFor(Exception exception) =>
        _handlers.FirstOrDefault(h => h.CanHandle(exception)) ?? _unhandledExceptionHandler;

    Microsoft.AspNetCore.Mvc.ProblemDetails ToProblemDetails(ExceptionInfo exceptionInfo) =>
        new()
        {
            Type = _settings.TypeUrlFormat is not null
                ? string.Format(_settings.TypeUrlFormat.GetValue(), NameOf(exceptionInfo.Exception).Kebaberize())
                : null,
            Title = NameOf(exceptionInfo.Exception).Titleize(),
            Status = exceptionInfo.Code,
            Detail = GetMessage(exceptionInfo),
            Extensions = exceptionInfo.ExtraData ?? []
        };

    string GetMessage(ExceptionInfo info) =>
        info.LKey is null
        ? info.Body
        : (_localizer[info.LKey, info.LParams ?? []] is var value && value == info.LKey
            ? info.Body
            : value
        );

    string NameOf(Exception exception) =>
        _localizer[exception.GetType().Name.Replace(nameof(Exception), string.Empty)];
}