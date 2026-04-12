using System.Globalization;

namespace Baked;

public class InvariantCultureScope : IDisposable
{
    readonly CultureInfo _current;

    public InvariantCultureScope()
    {
        _current = CultureInfo.CurrentCulture;

        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
    }

    public void Dispose() =>
        CultureInfo.CurrentCulture = _current;
}