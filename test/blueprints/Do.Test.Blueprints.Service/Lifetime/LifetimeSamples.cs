namespace Do.Test.Lifetime;

public class LifetimeSamples(
    Singleton _singleton,
    Func<ScopedContext> _getScoped,
    Func<Transient> _newTransient,
    Func<TransientAsync> _newTransientAsync,
    Func<TransientGeneric<Singleton>> _newTransientGeneric
)
{
    public Singleton Singleton() =>
        _singleton;

    public ScopedContext Scoped() =>
        _getScoped();

    public Transient Transient() =>
         _newTransient().With();

    public async Task<TransientAsync> TransientAsync() =>
        await _newTransientAsync().With();

    public TransientGeneric<Singleton> TransientGeneric() =>
        _newTransientGeneric().With();
}