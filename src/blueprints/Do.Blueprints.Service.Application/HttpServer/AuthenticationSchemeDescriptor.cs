using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Do.HttpServer;

public record AuthenticationSchemeDescriptor
(
    string Name,
    Action<AuthenticationBuilder> UseBuilder,
    Func<HttpContext, bool> Handles
);