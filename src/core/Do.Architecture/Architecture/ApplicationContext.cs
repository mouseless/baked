namespace Do.Architecture;

public class ApplicationContext
{
    readonly Dictionary<Type, object> _context = new();

    public void Add<T>(T item) where T : notnull => _context[typeof(T)] = item;

    public T Get<T>()
    {
        if (!Has<T>())
        {
            throw BuildKeyNotFound<T>();
        }

        return (T)_context[typeof(T)];
    }

    public bool Has<T>() => _context.ContainsKey(typeof(T));

    private KeyNotFoundException BuildKeyNotFound<T>()
    {
        var message = $"'{typeof(T).Name}' does not exist in context.";

        if (_context.Count == 0)
        {
            message = message.Replace('.', ' ') + "because it is empty.";

            return new(message);
        }

        var foundTypes = _context.Keys.Where(k => typeof(T).IsAssignableFrom(k)).ToList();
        if (foundTypes.Any())
        {
            var typeNames = foundTypes.Select(f => $"'{f.Name}'").ToList();
            message += " Did you mean: " + string.Join(", ", typeNames) + "?";

            return new(message);
        }

        var types = _context.Keys.Select(k => $"'{k.Name}'").ToList();
        message += " Available types are: " + string.Join(", ", types);

        return new(message);
    }
}
