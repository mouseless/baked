namespace Baked.Test.Lifetime;

public class TransientGenericUsage(
    Func<TransientGeneric<Singleton>> _newTransientGeneric
)
{
    internal TransientGeneric<Singleton> TransientGeneric() =>
        _newTransientGeneric().With();
}