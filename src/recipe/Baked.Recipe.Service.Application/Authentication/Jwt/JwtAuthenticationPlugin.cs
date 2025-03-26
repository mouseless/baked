using Baked.Ui;

namespace Baked.Authentication.Jwt;

public class JwtAuthenticationPlugin : IPlugin
{
    public string Name => "auth";
    public string TokenComposable => "useToken";
}
