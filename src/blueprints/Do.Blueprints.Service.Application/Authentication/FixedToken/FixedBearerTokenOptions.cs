using Microsoft.AspNetCore.Authentication;

namespace Do.Authentication.FixedToken;

public class FixedBearerTokenOptions : AuthenticationSchemeOptions
{
    public List<string> TokenNames { get; } = [];
}