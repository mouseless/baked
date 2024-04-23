
namespace Do.Authentication;

public class ClaimsPrincipalFactoryOptions(Dictionary<string, List<IClaimProvider>> _options)
    : IEnumerable<KeyValuePair<string, List<IClaimProvider>>>
{
    public IEnumerator<KeyValuePair<string, List<IClaimProvider>>> GetEnumerator() =>
        ((IEnumerable<KeyValuePair<string, List<IClaimProvider>>>)_options).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        ((IEnumerable)_options).GetEnumerator();
}