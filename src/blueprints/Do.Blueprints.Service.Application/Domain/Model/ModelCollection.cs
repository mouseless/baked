using System.Diagnostics.CodeAnalysis;

namespace Do.Domain.Model;

public class ModelCollection<T>() : IEnumerable<T>
    where T : IModel
{
    public static implicit operator ModelCollection<T>(List<T> other) => new(other);

    readonly Dictionary<string, T> _models = [];

    public ModelCollection(List<T> data)
        : this() => _models = data.ToDictionary(m => m.Id, m => m);

    List<T> List => _models.Values.ToList();
    public IEnumerator<T> GetEnumerator() => List.GetEnumerator();

    public void Add(T model) => _models[model.Id] = model;
    public T Get(string id) => _models[id];

    public bool TryGetValue(string id, [NotNullWhen(true)] out T? model) =>
        _models.TryGetValue(id, out model);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}