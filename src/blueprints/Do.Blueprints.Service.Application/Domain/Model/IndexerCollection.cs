
namespace Do.Domain.Model;

internal class IndexerCollection(List<IIndexer> _indexers)
{
    internal void Apply<T>(ModelCollection<T> collection) where T : IModelWithMetadata
    {
        foreach (var indexer in _indexers)
        {
            collection.CreateIndex(indexer);
        }
    }
}
