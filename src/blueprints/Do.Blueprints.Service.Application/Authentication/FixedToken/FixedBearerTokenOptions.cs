namespace Do.Authentication.FixedToken;

public class FixedBearerTokenOptions
{
    public List<string> TokenNames { get; init; } = [];
    public ClaimsPrincipalFactoryOptions ClaimsPrincipalFactoryOptions { get; init; } = [];
}