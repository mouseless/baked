using System.Collections.Concurrent;

namespace Baked.Business;

public static class Caster
{
    static readonly ConcurrentDictionary<Type, ConcurrentDictionary<Type, Func<IServiceProvider, object>>> _from = [];
    static IServiceProvider? _serviceProvider;

    public static void SetServiceProvider(IServiceProvider serviceProvider) =>
        _serviceProvider = serviceProvider;

    static IServiceProvider ServiceProvider => _serviceProvider ?? throw new InvalidOperationException("Cannot use Caster before setting IServiceProvider");

    public static void Add(Type fromType, Type toType, Func<IServiceProvider, object> getCaster)
    {
        if (!_from.TryGetValue(fromType, out var to))
        {
            _from[fromType] = to = [];
        }

        to[toType] = getCaster;
    }

    public static Casting<TFrom> Cast<TFrom>(this TFrom from) =>
        new(from);

    public class Casting<TFrom>(TFrom _from)
    {
        public TTo To<TTo>()
        {
            if (!Caster._from.TryGetValue(typeof(TFrom), out var to)) { throw new InvalidCastException($"Cannot cast {typeof(TFrom)} to {typeof(TTo)}"); }
            if (!to.TryGetValue(typeof(TTo), out var getCaster)) { throw new InvalidCastException($"Cannot cast {typeof(TFrom)} to {typeof(TTo)}"); }

            return ((ICasts<TFrom, TTo>)getCaster(ServiceProvider)).To(_from);
        }
    }
}