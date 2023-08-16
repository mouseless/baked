namespace Do.Architecture;

public class ApplicationContext
{
    readonly Dictionary<Type, object> _context = new();

    public void Add<T>(T item) where T : notnull => _context[typeof(T)] = item;

    public bool Has<T>() => _context.ContainsKey(typeof(T));

    public T Get<T>()
    {
        if (!Has<T>())
        {
            throw new KeyNotFoundException($"'{typeof(T).Name}' does not exist in context, " +
                $"{BuildMessageDetails<T>()}"
            );
        }

        return (T)_context[typeof(T)];
    }

    private string BuildMessageDetails<T>() =>
        !_context.Any()
            ? "because it is empty."
            : GetAssignableTypes<T>().Any()
                ? $"did you mean: {string.Join(", ", GetAssignableTypes<T>().Select(t => $"'{t.Name}'"))}?"
                : $"available types are: {string.Join(", ", _context.Keys.Select(t => $"'{t.Name}'"))}.";

    private IEnumerable<Type> GetAssignableTypes<T>() => _context.Keys.Where(t => t.IsAssignableTo(typeof(T)));
}
