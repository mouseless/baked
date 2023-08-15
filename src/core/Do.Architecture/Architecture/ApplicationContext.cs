namespace Do.Architecture;

public class ApplicationContext
{
    readonly Dictionary<Type, object> _context = new();

    public void Add<T>(T item) where T : notnull => _context[typeof(T)] = item;

    public T Get<T>()
    {
        if (!Has<T>())
        {
            throw new KeyNotFoundException(MessageBuilder<T>());
        }

        return (T)_context[typeof(T)];
    }

    public bool Has<T>() => _context.ContainsKey(typeof(T));

    private string MessageBuilder<T>()
    {
        var message = $"'{typeof(T).Name}' does not exist in context.";

        if (_context.Count == 0) return message.Replace('.', ' ') + "because it is empty.";

        var foundTypes = _context.Keys.Where(k => typeof(T).IsAssignableFrom(k)).ToList();
        if (foundTypes.Any())
        {
            var typeNames = foundTypes.Select(f => $"'{f.Name}'").ToList();

            return message += " Did you mean: " + string.Join(", ", typeNames) + "?";
        }

        var types = _context.Keys.Select(k => $"'{k.Name}'").ToList();

        return message += " Available types are: " + string.Join(", ", types);
    }
}
