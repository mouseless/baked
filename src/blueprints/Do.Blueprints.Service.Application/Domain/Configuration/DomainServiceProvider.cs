namespace Do.Domain.Configuration;

public class DomainServiceProvider
{
    readonly Dictionary<Type, Func<DomainServiceProvider, object>> _factories = [];
    readonly Dictionary<Type, object> _instances = [];

    internal void Add(Type type, Func<DomainServiceProvider, object> factory) => _factories[type] = factory;

    internal T Get<T>() where T : class => (T)GetDomainService(typeof(T));

    internal object GetDomainService(Type type)
    {
        if (!_factories.TryGetValue(type, out Func<DomainServiceProvider, object>? factory))
        {
            throw new KeyNotFoundException($"No domain service for '{type.Name}'is registered");
        }

        return _instances.TryGetValue(type, out object? value) ? value : (_instances[type] = factory.Invoke(this));
    }
}


