
namespace Do.Authentication;

public class ClaimsPrincipalFactoryOptions : IEnumerable<KeyValuePair<string, List<IClaimProvider>>>
{
    readonly Dictionary<string, List<IClaimProvider>> _options = [];

    public void AddIdentity(string name, List<IClaimProvider> claimProviders)
    {
        if (!_options.ContainsKey(name))
        {
            _options[name] = [];
        }

        _options[name].AddRange(claimProviders);
    }

    public IEnumerator<KeyValuePair<string, List<IClaimProvider>>> GetEnumerator() =>
        ((IEnumerable<KeyValuePair<string, List<IClaimProvider>>>)_options).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable)_options).GetEnumerator();
}