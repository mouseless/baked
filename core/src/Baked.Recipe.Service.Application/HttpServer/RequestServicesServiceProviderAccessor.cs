using Baked.Runtime;
using Microsoft.AspNetCore.Http;

namespace Baked.HttpServer;

public class RequestServicesServiceProviderAccessor(IHttpContextAccessor contextAccessor)
    : IServiceProviderAccessor
{
    public IServiceProvider? GetServiceProvider() =>
        contextAccessor.HttpContext?.RequestServices;
}