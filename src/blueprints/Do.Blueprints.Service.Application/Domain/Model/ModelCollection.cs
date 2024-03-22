using System.Diagnostics.CodeAnalysis;

namespace Do.Domain.Model;

public class ModelCollection<T> : IEnumerable<T>
    where T : IModel
{
    readonly KeyedModelCollection<T> _models = [];

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

    public IEnumerator<T> GetEnumerator() => _models.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class IndexedModelCollection<T> : ModelCollection<T> where T : IModelWithMetadata
{
    readonly ModelIndex<T> _index = [];
    readonly List<AttributeIndexer> _indexers = [];

    internal IndexedModelCollection() { }

    internal IndexedModelCollection(IEnumerable<T> models, List<AttributeIndexer> indexers)
        : base()
    {
        _indexers = indexers;

        foreach (var model in models)
        {
            foreach (var indexer in indexers)
            {
                indexer.Execute(_index, model);
            }

            Models.Add(model);
        }
    }

    public ModelCollection<T> GetIndex(string id) =>
        _index.GetOrEmpty(id);
}

internal class ModelCollectionFactory<T, TCollection>(List<AttributeIndexer> _indexers)
    where T : IModelWithMetadata
    where TCollection : IndexedModelCollection<T>, IIndexedModelCollection<T, TCollection>
{
    internal TCollection Create(IEnumerable<T> models) =>
        TCollection.New(models, _indexers);
}

internal interface IIndexedModelCollection<TModel, TCollection>
    where TModel : IModelWithMetadata
    where TCollection : IndexedModelCollection<TModel>
{
    static abstract TCollection New(IEnumerable<TModel> models, List<AttributeIndexer> _indexers);
}