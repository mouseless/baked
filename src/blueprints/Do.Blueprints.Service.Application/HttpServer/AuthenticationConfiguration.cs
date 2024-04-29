using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Do.HttpServer;

public record AuthenticationConfiguration
(
    string Scheme,
    Action<AuthenticationOptions> ConfigureAuthentication,
    Func<HttpContext, bool> ShouldHandle
);