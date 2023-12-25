using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Do.ExceptionHandling.Default;

public class ExceptionHandlingMiddleware(IEnumerable<IExceptionHandler> _handlers, IConfiguration _configuration)
    : Microsoft.AspNetCore.Diagnostics.IExceptionHandler
{
    readonly UnhandledExceptionHandler _unhandledExceptionHandler = new();

    string ExceptionTypeUrl => _configuration.GetValue<string>("Exception:TypeUrl") ?? string.Empty;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var handler = _handlers.FirstOrDefault(h => h.CanHandle(exception)) ?? _unhandledExceptionHandler;
        ExceptionInfo exceptionInfo = handler.Handle(exception);

        var problemDetails = new ProblemDetails
        {
            Type = string.Format(ExceptionTypeUrl, exception.ExceptionId()),
            Title = exception.ExceptionName(),
            Status = exceptionInfo.Code,
            Detail = exceptionInfo.Body,
            Extensions = exceptionInfo.ExtraData ?? []
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = exceptionInfo.Code;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
