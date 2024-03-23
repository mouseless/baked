namespace Do.Domain.Configuration;

public class DomainBuilderContext
{
    readonly Dictionary<Type, Func<DomainBuilderContext, object>> _factories = [];
    readonly Dictionary<Type, object> _instances = [];

    public void Add<T>(T item) where T : notnull => _factories[typeof(T)] = _ => item;
    public void Add<T>() where T : IDomainComponent => _factories[typeof(T)] = T.New;

    public T Get<T>() where T : class
    {
        var type = typeof(T);

        if (!_factories.ContainsKey(type))
        {
            throw new KeyNotFoundException($"'{typeof(T).Name}' does not exist in context");
        }

        return _instances.ContainsKey(type) ? (T)_instances[type] : (T)(_instances[type] = _factories[type].Invoke(this));
    }
}
