using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Do.HttpServer;

public record AuthenticationDescriptor(
    string Scheme,
    Action<AuthenticationBuilder> UseBuilder,
    Func<HttpContext, bool> Handles
);