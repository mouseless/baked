using Baked.Ui;

namespace Baked.Authentication.Jwt;

public record JwtAuthenticationPlugin : PluginBase
{
    public override string Name => "auth";
    public List<string> AnonymousApiRoutes { get; init; } = [];
    public List<string> AnonymousPageRoutes { get; init; } = [];
    public string LoginPageRoute { get; set; } = "auth/login";
    public string RefreshApiRoute { get; set; } = "auth/refresh";
}