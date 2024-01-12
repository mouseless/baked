using System.Diagnostics.CodeAnalysis;

namespace Do.Domain.Model;

public class ModelCollection<T>() : IEnumerable<T>
    where T : IModel
{
    public static implicit operator ModelCollection<T>(List<T> other) => new(other);

    readonly Dictionary<string, T> _models = [];

    public ModelCollection(List<T> data)
        : this() => _models = data.ToDictionary(m => m.Name, m => m);

    List<T> List => _models.Values.ToList();
    public IEnumerator<T> GetEnumerator() => List.GetEnumerator();

    public T Add(T model) => _models[model.Name] = model;
    public T Get(string name) => _models[name];

    public bool TryGetValue(string name, [NotNullWhen(true)] out T? model) =>
        _models.TryGetValue(name, out model);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}