using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Baked.ExceptionHandling.ProblemDetails;

public class ExceptionLoggerMiddleware(ILogger<ExceptionLoggerMiddleware> _logger, RequestDelegate _next)
{
    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        var feature = context.Features.Get<IExceptionHandlerFeature>();
        if (feature?.Error is null) { return; }

        _logger.Log(
            context.Response.StatusCode >= 500 ? LogLevel.Error : LogLevel.Warning,
            feature?.Error,
            feature?.Error.Message
        );
    }
}