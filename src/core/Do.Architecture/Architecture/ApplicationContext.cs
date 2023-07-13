namespace Do.Architecture;

public class ApplicationContext
{
    readonly Dictionary<Type, object> _context = new();

    public void Add<T>(T item) where T : notnull => _context[typeof(T)] = item;
    public T Get<T>() => (T)_context[typeof(T)];
    public bool Has<T>() => _context.ContainsKey(typeof(T));
}
