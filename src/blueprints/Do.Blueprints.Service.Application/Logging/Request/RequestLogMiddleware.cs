using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NHibernate.Exceptions;

namespace Do.Logging.Request;

public class RequestLogMiddleware(ILogger<RequestLogMiddleware> _logger, RequestDelegate _next)
{
    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation(message: $"Begin: {context.Request.Path}");

        await _next(context);

        var exception = context.Features.Get<IExceptionHandlerFeature>();

        if (exception is not null && exception.Error is not null)
        {
            if (exception.Error is GenericADOException genericADOException)
            {
                _logger.LogError(exception: genericADOException.InnerException, message: genericADOException.InnerException!.Message);
            }
            else
            {
                _logger.LogError(exception: exception.Error, message: exception.Error.Message);
            }
        }
        else
        {
            _logger.LogInformation(message: $"End: {context.Request.Path} StatusCode: {context.Response.StatusCode}");
        }
    }
}
