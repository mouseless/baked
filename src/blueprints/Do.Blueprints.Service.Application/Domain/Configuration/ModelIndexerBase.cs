using Do.Domain.Model;

namespace Do.Domain.Configuration;

public abstract class ModelIndexerBase : IModelIndexer
{
    protected abstract ModelIndexKey IndexKey { get; }
    protected abstract bool AppliesTo(IModel model);

    void Apply<T>(ModelIndex<T> index, T model) where T : IModel
    {
        if (!index.ContainsKey(IndexKey))
        {
            index[IndexKey] = [];
        }

        index[IndexKey].TryAdd(model);
    }

    bool IModelIndexer.AppliestTo(IModel model) => AppliesTo(model);
    void IModelIndexer.Apply<T>(IIndexedCollection<T> collection, T model) => Apply(collection.Index, model);
}
