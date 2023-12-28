﻿using Microsoft.AspNetCore.Http;

namespace Do.ExceptionHandling.Default;

public class ExceptionHandlingMiddleware
{
    readonly RequestDelegate _next;
    readonly IEnumerable<IExceptionHandler> _handlers;
    readonly UnhandledExceptionHandler _unhandledExceptionHandler = new();

    public ExceptionHandlingMiddleware(IEnumerable<IExceptionHandler> handlers, RequestDelegate next) =>
        (_handlers, _next) = (handlers, next);

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