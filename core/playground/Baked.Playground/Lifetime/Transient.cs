namespace Baked.Playground.Lifetime;

public class Transient : ITransientInterface
{
    internal Transient With() =>
        this;
}