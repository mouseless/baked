namespace Do.Authentication;

public class ClaimsPrincipalProviderOptions
{
    public Dictionary<string, List<IClaimProvider>> IdentityOptions { get; } = [];
}