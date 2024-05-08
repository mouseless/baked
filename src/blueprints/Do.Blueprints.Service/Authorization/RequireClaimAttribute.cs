namespace Do.Authorization;

public class RequireClaimAttribute(string claim) : Attribute
{
    public string Claim => claim;
}