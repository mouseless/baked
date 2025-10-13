namespace Baked.Test.Lifetime;

public class Transient : ITransientInterface
{
    internal Transient With() =>
        this;
}