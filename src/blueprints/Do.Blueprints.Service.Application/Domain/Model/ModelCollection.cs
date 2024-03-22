using System.Diagnostics.CodeAnalysis;

namespace Do.Domain.Model;

public class ModelCollection<T> : IEnumerable<T>
    where T : IModel
{
    readonly KeyedModelCollection<T> _models = [];
    readonly ModelIndex<T> _index = [];

    public ModelCollection() { }

    public ModelCollection(IEnumerable<T> models)
    {
        foreach (var model in models)
        {
            _models.Add(model);
        }
    }

    protected KeyedModelCollection<T> Models => _models;

    public T this[string id] =>
        _models[id];

    public int Count => _models.Count;

    public bool ContainsModel(T? model) =>
        _models.Contains(model?.Id ?? string.Empty);

    public bool Contains(string id) =>
        _models.Contains(id);

    public bool TryGetValue(string id, [NotNullWhen(true)] out T? model) =>
       _models.TryGetValue(id, out model);

    internal bool TryAdd(T typeModel)
    {
        if (!ContainsModel(typeModel))
        {
            _models.Add(typeModel);

            return true;
        }

        return false;
    }

    public ModelCollection<T> GetIndex(string id) =>
        _index.GetOrEmpty(id);

    internal void CreateIndex(IIndexer indexer)
    {
        foreach (var model in Models)
        {
            indexer.Execute(_index, model);
        }
    }

    public IEnumerator<T> GetEnumerator() => _models.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
