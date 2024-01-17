using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Do.Domain.Model;

public class ModelCollection<T>() : IEnumerable<T>
    where T : IModel
{
    readonly KeyedModelCollection<T> _models = [];

    public ModelCollection(IEnumerable<T> data)
        : this() => AddRange(data);

    public T this[string key] =>
        _models[key];

    public bool TryGetValue(string id, [NotNullWhen(true)] out T? model) =>
       _models.TryGetValue(id, out model);

    void AddRange(IEnumerable<T> models)
    {
        foreach (var model in models)
        {
            _models.Add(model);
        }
    }

    public IEnumerator<T> GetEnumerator() => _models.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    class KeyedModelCollection<TItem> : KeyedCollection<string, TItem>
        where TItem : IModel
    {
        protected override string GetKeyForItem(TItem item) => item.Id;
    }
}
