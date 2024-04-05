using Do.Test.Orm;

namespace Do.Test.Business;

public class DependencyInjectionSamples(
    Singleton _singleton,
    Func<Scoped> _getScoped,
    Func<Transient> _newTransient,
    Func<TransientAsync> _newTransientAsync,
    Func<TransientGeneric<Entity>> _newTransientGeneric
) : SingletonBase, IInterface
{
    public Singleton Singleton() =>
        _singleton;

    public Scoped Scoped() =>
        _getScoped();

    public Transient Transient() =>
         _newTransient().With();

    public async Task<TransientAsync> TransientAsync() =>
        await _newTransientAsync().With();

    public TransientGeneric<Entity> TransientGeneric() =>
        _newTransientGeneric().With();
}
