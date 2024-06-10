namespace Do.Authentication.FixedBearerToken;

public class TokenOptions(IEnumerable<Token> _tokens)
{
    public IEnumerable<Token> Tokens => _tokens;
}