using Do.Domain.Model;

namespace Do.Domain.Configuration;

public abstract class IndexerBase : IIndexer
{
    protected abstract string IndexId { get; }
    protected abstract bool AppliesTo(IModel model);

    void Apply<T>(ModelIndex<T> index, T model) where T : IModel
    {
        if (!index.ContainsKey(IndexId))
        {
            index[IndexId] = [];
        }

        index[IndexId].TryAdd(model);
    }

    bool IIndexer.AppliestTo(IModel model) => AppliesTo(model);
    void IIndexer.Apply<T>(IIndexedCollection<T> collection, T model) => Apply(collection.Index, model);
}
