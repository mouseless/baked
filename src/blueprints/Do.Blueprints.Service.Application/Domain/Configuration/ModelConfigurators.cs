namespace Do.Domain.Configuration;

public class ModelConfigurators
{
    readonly Dictionary<Type, object> _context = [];

    public void Add<T>(T item) where T : notnull => _context[typeof(T)] = item;

    public bool Has<T>() => _context.ContainsKey(typeof(T));

    public T Get<T>()
    {
        if (!Has<T>())
        {
            throw new KeyNotFoundException($"'{typeof(T).Name}' does not exist in context");
        }

        return (T)_context[typeof(T)];
    }
}
