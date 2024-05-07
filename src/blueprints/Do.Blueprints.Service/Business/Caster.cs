using System.Collections.Concurrent;

namespace Do.Business;

public static class Caster
{
    static readonly ConcurrentDictionary<Type, ConcurrentDictionary<Type, object>> _all = [];

    public static void Add(Type from, Type to, object caster)
    {
        if (!_all.TryGetValue(from, out var fromCasters))
        {
            _all[from] = fromCasters = [];
        }

        fromCasters.TryAdd(to, caster);
    }

    public static Casting<TFrom> Cast<TFrom>(this TFrom from) =>
        new(from);

    public class Casting<TFrom>(TFrom _from)
    {
        public TTo To<TTo>()
        {
            if (!_all.TryGetValue(typeof(TFrom), out var to)) { throw new InvalidCastException($"Cannot cast {typeof(TFrom)} to {typeof(TTo)}"); }
            if (!to.TryGetValue(typeof(TTo), out var caster)) { throw new InvalidCastException($"Cannot cast {typeof(TFrom)} to {typeof(TTo)}"); }

            return ((ICasts<TFrom, TTo>)caster).To(_from);
        }
    }
}