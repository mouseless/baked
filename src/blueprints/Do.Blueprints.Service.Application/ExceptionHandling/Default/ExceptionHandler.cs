using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Do.ExceptionHandling.Default;

public class ExceptionHandler(IEnumerable<IExceptionHandler> _handlers, ExceptionHandlerSettings _settings)
    : Microsoft.AspNetCore.Diagnostics.IExceptionHandler
{
    readonly UnhandledExceptionHandler _unhandledExceptionHandler = new();

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

    ProblemDetails ToProblemDetails(ExceptionInfo exceptionInfo) =>
        new()
        {
            Type = _settings.TypeUrlFormat is not null
                ? string.Format(_settings.TypeUrlFormat.GetValue(), SnakeCase(NameOf(exceptionInfo.Exception)))
                : null,
            Title = TitleCase(NameOf(exceptionInfo.Exception)),
            Status = exceptionInfo.Code,
            Detail = exceptionInfo.Body,
            Extensions = exceptionInfo.ExtraData ?? []
        };

    string NameOf(Exception exception) =>
        exception.GetType().Name.Replace(nameof(Exception), string.Empty);

    string TitleCase(string pascalCaseString) =>
        SplitPascalCase(pascalCaseString, joinBy: " ");

    string SnakeCase(string pascalCaseString) =>
        SplitPascalCase(pascalCaseString, joinBy: "-").ToLowerInvariant();

    string SplitPascalCase(string @string, string joinBy) =>
        Regexes.PascalCaseSplitter().Replace(@string, joinBy);
}
