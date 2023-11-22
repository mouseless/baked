using Microsoft.AspNetCore.Http;
using NHibernate.Exceptions;

namespace Do.ExceptionHandling.Default;

public class ExceptionHandlingNHibernate
{
    readonly RequestDelegate _next;
    readonly IEnumerable<UnhandledExceptionHandler> _handlers;
    readonly UnhandledExceptionHandler _unhandledExceptionHandler = new();

    public ExceptionHandlingNHibernate(IEnumerable<UnhandledExceptionHandler> handlers, RequestDelegate next) =>
        (_handlers, _next) = (handlers, next);

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (GenericADOException ex)
        {
            var handler = _handlers.FirstOrDefault(h => h.CanHandle(ex)) ?? _unhandledExceptionHandler;
            var baseExceptionInfo = handler.Handle(ex);
            var message = ex.InnerException != null
                ? ex.InnerException.Message
                : baseExceptionInfo.Body.ToString();

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = baseExceptionInfo.Code;

            object forwardedExceptionInfo =
                new
                {
                    Request = context.Request.Path.Value,
                    Message = message
                };

            await context.Response.WriteAsJsonAsync(forwardedExceptionInfo);
        }
    }
}
