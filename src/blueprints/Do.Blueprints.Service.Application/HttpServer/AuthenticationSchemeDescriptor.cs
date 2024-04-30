using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Do.HttpServer;

public record AuthenticationSchemeDescriptor
(
    string Name,
    Func<HttpContext, bool> Handles,
    Action<AuthenticationOptions>? ConfigureOptions,
    Action<AuthenticationBuilder>? UseBuilder
);