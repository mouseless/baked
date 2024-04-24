namespace Do.Test.Lifetime;

public class Transient : ITransientInterface
{
    internal Transient With() =>
        this;
}