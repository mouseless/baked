using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Do.ExceptionHandling.Default;

public class ExceptionHandler(IEnumerable<IExceptionHandler> _handlers, Func<ExceptionConfig> _exceptionConfig)
    : Microsoft.AspNetCore.Diagnostics.IExceptionHandler
{
    readonly UnhandledExceptionHandler _unhandledExceptionHandler = new();

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var handler = _handlers.FirstOrDefault(h => h.CanHandle(exception)) ?? _unhandledExceptionHandler;
        ExceptionInfo exceptionInfo = handler.Handle(exception);

        var type = string.Format(_exceptionConfig().TypeUrl ?? string.Empty, ExceptionId(exception));
        var problemDetails = new ProblemDetails
        {
            Type = type == string.Empty ? null : type,
            Title = ExceptionName(exception),
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

    string ExceptionName(Exception exception)
    {
        var wordsSeparatedBySpace = string.Join(" ", exception.GetType().Name.SplitWordsFromCapitalLetters());
        var result = wordsSeparatedBySpace.Replace(" Exception", string.Empty);

        return result;
    }

    string ExceptionId(Exception exception)
    {
        var exceptionName = ExceptionName(exception);
        var result = exceptionName.Replace(" ", "-").ToLower();

        return result;
    }
}
