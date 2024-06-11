using Microsoft.AspNetCore.Builder;

namespace Baked.HttpServer;

public record MiddlewareDescriptor(Action<IApplicationBuilder> Configure, int Order);