using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Do.ExceptionHandling.Default;

public class ExceptionHandlingMiddleware(IEnumerable<IExceptionHandler> _handlers)
    : Microsoft.AspNetCore.Diagnostics.IExceptionHandler
{
    readonly UnhandledExceptionHandler _unhandledExceptionHandler = new();

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var handler = _handlers.FirstOrDefault(h => h.CanHandle(exception)) ?? _unhandledExceptionHandler;
        ExceptionInfo exceptionInfo = handler.Handle(exception);

        var problemDetails = new ProblemDetails
        {
            Type = $"this-url-should-be-read-from-config/exceptions/{exception.ExceptionId()}",
            Title = exception.ExceptionName(),
            Status = exceptionInfo.Code,
            Detail = exceptionInfo.Body.ToString(), // TODO - body string olmalı
            Extensions = new Dictionary<string, object?>()
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = exceptionInfo.Code;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
