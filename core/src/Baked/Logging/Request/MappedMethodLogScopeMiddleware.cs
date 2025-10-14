using Baked.Binding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;

namespace Baked.Logging.Request;

public class MappedMethodLogScopeMiddleware(ILogger<MappedMethodLogScopeMiddleware> _logger, RequestDelegate _next)
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
            await _next(context);
        }
    }
}