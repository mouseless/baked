using Baked.Ui;

namespace Baked.Authentication.Jwt;

public class JwtAuthenticationPlugin()
    : IPlugin
{
    public string Name => "auth";
    public List<string> AnonymousRoutes { get; set; } = [];
    public string LoginPath { get; set; } = "auth/login";
    public string RefreshPath { get; set; } = "auth/refresh";
    public string LoginPage { get; set; } = "auth/login";
}