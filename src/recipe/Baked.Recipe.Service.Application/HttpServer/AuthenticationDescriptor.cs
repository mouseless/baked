using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Baked.HttpServer;

public record AuthenticationDescriptor(
    string Scheme,
    Action<AuthenticationBuilder> UseBuilder,
    Func<HttpContext, bool> Handles
);