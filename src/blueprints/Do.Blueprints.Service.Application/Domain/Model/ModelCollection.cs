using System.Diagnostics.CodeAnalysis;

namespace Do.Domain.Model;

public class ModelCollection<T> : IEnumerable<T>
    where T : IModel
{
    readonly KeyedModelCollection<T> _models = [];

    public ModelCollection(IEnumerable<T> data)
    {
        foreach (var model in data)
        {
            _models.Add(model);
        }
    }

    public T this[string key] =>
        _models[key];

    public bool Contains(T? item) =>
        _models.Contains(item?.Id ?? string.Empty);

    public bool TryGetValue(string id, [NotNullWhen(true)] out T? model) =>
       _models.TryGetValue(id, out model);

    public IEnumerator<T> GetEnumerator() => _models.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
