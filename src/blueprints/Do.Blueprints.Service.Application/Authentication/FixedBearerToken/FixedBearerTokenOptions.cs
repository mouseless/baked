namespace Do.Authentication.FixedBearerToken;

public class FixedBearerTokenOptions
{
    readonly Dictionary<string, List<string>> _tokenClaims = [];

    public List<string> TokenNames => [.. _tokenClaims.Keys];

    public void Add(string tokenName, List<string> claims)
    {
        _tokenClaims.Add(tokenName, claims);
    }

    public List<string> GetClaims(string tokenName) =>
        _tokenClaims.TryGetValue(tokenName, out var claims) ? claims : [];
}