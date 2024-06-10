namespace Do.Test.Lifetime;

public class TransientGeneric<T>
{
    internal TransientGeneric<T> With() =>
        this;
}