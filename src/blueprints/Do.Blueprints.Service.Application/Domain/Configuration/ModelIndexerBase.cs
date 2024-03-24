using Do.Domain.Model;

namespace Do.Domain.Configuration;

public abstract class ModelIndexerBase : IModelIndexer
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

    bool IModelIndexer.AppliestTo(IModel model) => AppliesTo(model);
    void IModelIndexer.Apply<T>(IIndexedCollection<T> collection, T model) => Apply(collection.Index, model);
}
