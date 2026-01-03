namespace Baked.Playground.Lifetime;

public class TransientGeneric<T>
{
    internal TransientGeneric<T> With() =>
        this;
}