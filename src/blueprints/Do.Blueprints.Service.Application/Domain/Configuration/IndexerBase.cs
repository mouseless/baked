using Do.Domain.Model;

namespace Do.Domain.Configuration;

public abstract class IndexerBase : IIndexer
{
    protected abstract string IndexId { get; }
    protected abstract bool CanIndex(IModel model);

    internal void Execute<T>(ModelIndex<T> index, T model) where T : IModel
    {
        if (CanIndex(model))
        {
            if (!index.ContainsKey(IndexId))
            {
                index[IndexId] = [];
            }

            index[IndexId].TryAdd(model);
        }
    }

    void IIndexer.Execute<T>(ModelIndex<T> index, T model) => Execute(index, model);
}
