namespace Do.Authentication.FixedBearerToken;

public class FixedBearerTokenOptions
{
    readonly List<Token> _tokens = [];

    public List<Token> Tokens => _tokens;

    public void Add(string tokenName, List<string> claims)
    {
        _tokens.Add(new(tokenName, claims));
    }
}