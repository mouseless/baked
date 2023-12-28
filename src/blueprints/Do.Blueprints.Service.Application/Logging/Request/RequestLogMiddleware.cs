using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Do.Logging.Request;

public class RequestLogMiddleware(ILogger<RequestLogMiddleware> _logger, RequestDelegate _next)
{
    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation(message: $"Begin: {context.Request.Path}");

        await _next(context);

        var exception = context.Features.Get<ExceptionHandlerFeature>();

        if (exception is not null && exception.Error is not null)
        {
            _logger.LogError(exception: exception.Error, message: exception.Error.Message);
        }

        _logger.LogInformation(message: $"End: {context.Request.Path} StatusCode: {context.Response.StatusCode}");
    }
}
