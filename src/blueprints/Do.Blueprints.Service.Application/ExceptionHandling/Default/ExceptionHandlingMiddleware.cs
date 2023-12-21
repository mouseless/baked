using Microsoft.AspNetCore.Http;

namespace Do.ExceptionHandling.Default;

public class ExceptionHandlingMiddleware(IEnumerable<IExceptionHandler> _handlers, RequestDelegate _next)
{
    readonly UnhandledExceptionHandler _unhandledExceptionHandler = new();

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var handler = _handlers.FirstOrDefault(h => h.CanHandle(ex)) ?? _unhandledExceptionHandler;
            var exceptionInfo = handler.Handle(ex);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exceptionInfo.Code;

            object forwardedExceptionInfo =
                new
                {
                    Request = context.Request.Path.Value,
                    Message = exceptionInfo.Body
                };

            await context.Response.WriteAsJsonAsync(forwardedExceptionInfo);
        }
    }
}
