namespace Do.Authentication.FixedBearerToken;

public class FixedBearerTokenOptions
{
    public List<Token> Tokens { get; } = [];

    public void Add(string tokenName, List<string> claims)
    {
        Tokens.Add(new(tokenName, claims));
    }
}