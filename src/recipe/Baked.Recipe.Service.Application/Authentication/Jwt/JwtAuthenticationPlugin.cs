using Baked.Ui;

namespace Baked.Authentication.Jwt;

public class JwtAuthenticationPlugin : IPlugin
{
    public string Name => "auth";
    public List<string> AnonymousRoutes { get; set; } = [];
    public string LoginPath { get; set; } = default!;
    public string RefreshPath { get; set; } = default!;
    public string LoginPage { get; set; } = default!;
}