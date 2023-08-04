using Do.Core;

namespace Do.Test;

public class Singleton
{
    readonly ISystem _system;

    public Singleton(ISystem system) =>
        _system = system;

    public DateTime GetNow() => _system.Now;
}
