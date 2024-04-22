namespace Do.Authentication;

public class ClaimsPrincipleProviderOptions
{
    public Dictionary<string, List<IClaimProvider>> IdentityOptions { get; } = [];
}