using Microsoft.AspNetCore.Builder;

namespace Do.HttpServer;

public record MiddlewareDescriptor(Action<IApplicationBuilder> Configure, int Order);
