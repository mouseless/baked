using Baked.Localization;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Baked.ExceptionHandling.ProblemDetails;

public class ExceptionHandler(IEnumerable<IExceptionHandler> _handlers, ExceptionHandlerSettings _settings, IServiceProvider _serviceProvider)
    : Microsoft.AspNetCore.Diagnostics.IExceptionHandler
{
    readonly UnhandledExceptionHandler _unhandledExceptionHandler = new(_settings);

    ILocalizer? Localizer => _serviceProvider.GetService<ILocalizer>();

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
            Detail = Localizer?[
                exceptionInfo.Body,
                exceptionInfo.ExtraData?.Values.OfType<object>().ToArray() ?? []
            ],
            Extensions = exceptionInfo.ExtraData ?? []
        };

    string NameOf(Exception exception) =>
        exception.GetType().Name.Replace(nameof(Exception), string.Empty);
}