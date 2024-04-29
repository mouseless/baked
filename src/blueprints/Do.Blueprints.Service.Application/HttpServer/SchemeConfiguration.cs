using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Do.HttpServer;

public record SchemeConfiguration
(
    string Name,
    Func<HttpContext, bool> ShouldHandle,
    Action<AuthenticationOptions>? Configure = default,
    Action<AuthenticationBuilder>? Builder = default
);