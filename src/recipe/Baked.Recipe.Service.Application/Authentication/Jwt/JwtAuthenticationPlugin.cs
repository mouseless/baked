using Baked.Ui;

namespace Baked.Authentication.Jwt;

public class JwtAuthenticationPlugin()
    : IPlugin
{
    public string Name => "auth";
    public List<string> AnonymousApiRoutes { get; init; } = [];
    public List<string> AnonymousPageRoutes { get; init; } = [];
    public string LoginPageRoute { get; set; } = "auth/login";
    public string RefreshApiRoute { get; set; } = "auth/refresh";
}