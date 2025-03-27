using Baked.Ui;

namespace Baked.Authentication.Jwt;

public class JwtAuthenticationPlugin : IPlugin
{
    public string Name => "auth";
    public string TokenComposable => "useToken";
    public List<string> AnonymousRoutes { get; set; } = [];
    public string LoginRoute { get; set; } = default!;
    public string RefreshRoute { get; set; } = default!;
}