using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class DomainIndexerCollection()
{
    readonly List<IIndexer> _indexers = [];

    public void Add(IIndexer indexer) =>
        _indexers.Add(indexer);

    internal void Apply<T>(ModelCollection<T> collection) where T : IModelWithMetadata
    {
        foreach (var indexer in _indexers)
        {
            foreach (var model in collection)
            {
                if (indexer.AppliestTo(model))
                {
                    indexer.Apply(collection, model);
                }
            }
        }
    }
}
