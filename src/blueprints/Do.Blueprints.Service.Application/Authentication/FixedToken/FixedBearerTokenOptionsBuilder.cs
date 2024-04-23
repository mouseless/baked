namespace Do.Authentication.FixedToken;

public class FixedBearerTokenOptionsBuilder
{
    List<string> _defaultTokenNames = ["Default"];
    List<string> _tokenNames = [];
    readonly Dictionary<string, List<IClaimProvider>> _identityOptions = [];

    public string DefaultIdentityName { get; set; } = "Default";
    public IClaimProvider DefaultClaimProvider { get; set; } = new TokenClaimProvider();

    public void AddIdentity(string name, IClaimProvider claimProvider) =>
        AddIdentity(name, [claimProvider]);

    public void AddIdentity(string name, List<IClaimProvider> claimProviders)
    {
        if (!_identityOptions.ContainsKey(name))
        {
            _identityOptions[name] = [];
        }

        _identityOptions[name].AddRange(claimProviders);
    }

    public FixedBearerTokenOptions Build()
    {
        if (!_tokenNames.Any())
        {
            _tokenNames.AddRange(_defaultTokenNames);
        }

        AddIdentity(DefaultIdentityName, DefaultClaimProvider);

        return new(_tokenNames, new(_identityOptions));
    }
}