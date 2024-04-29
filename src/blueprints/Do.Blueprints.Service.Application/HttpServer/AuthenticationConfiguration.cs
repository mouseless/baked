using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Do.HttpServer;

public record AuthenticationConfiguration
(
    string Scheme,
    Func<HttpContext, bool> ShouldHandle,
    Action<AuthenticationOptions>? ConfigureAuthentication = default,
    Action<AuthenticationBuilder>? UseBuilder = default
);