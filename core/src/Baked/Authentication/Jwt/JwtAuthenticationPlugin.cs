using Baked.Ui.Configuration;

namespace Baked.Authentication.Jwt;

public record JwtAuthenticationPlugin : ModulePluginBase
{
    public override string Name => "auth";
    public List<AnonymousApiRoute> AnonymousApiRoutes { get; init; } = [];
    public List<string> AnonymousPageRoutes { get; init; } = [];
    public string LoginPageRoute { get; set; } = "auth/login";
    public string RefreshApiRoute { get; set; } = "auth/refresh";
}