using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;

namespace Do.Logging.Request;

public class RequestLogMiddleware(ILogger<RequestLogMiddleware> _logger, RequestDelegate _next)
{
    public async Task Invoke(HttpContext context)
    {
        var mappedMethod = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata.GetMetadata<MappedMethodAttribute>();
        if (mappedMethod is null)
        {
            await _next(context);

            return;
        }

        using (_logger.BeginScope("Type:{0}, Method:{1}", mappedMethod.TypeFullName, mappedMethod.MethodName))
        {
            _logger.LogInformation(message: $"begin");

            await _next(context);

            _logger.LogInformation(message: $"end - {context.Response.StatusCode}");

        }
    }
}