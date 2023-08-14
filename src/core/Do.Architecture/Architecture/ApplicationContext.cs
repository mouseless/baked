namespace Do.Architecture;

public class ApplicationContext
{
    readonly Dictionary<Type, object> _context = new();

    public void Add<T>(T item) where T : notnull => _context[typeof(T)] = item;

    public T Get<T>()
    {
        if (Has<T>())
        {
            return (T)_context[typeof(T)];
        }

        if (_context.Count == 0)
        {
            throw new KeyNotFoundException("Context is empty.");
        }

        var types = _context.Keys.Select(k => k.Name).ToList();

        var foundTypes = _context.Keys.Where(k => typeof(T).IsAssignableFrom(k)).ToList();
        var typeNames = foundTypes.Select(f => f.Name).ToList();

        var message = typeNames.Count == 0 ? String.Join(", ", types) : String.Join(", ", typeNames);
        throw new KeyNotFoundException($"Given type could not be found in ApplicationContext. Did you mean {message} ?");
    }

    public bool Has<T>() => _context.ContainsKey(typeof(T));
}
