
namespace Do.Domain.Model;

internal class IndexerCollection(List<AttributeIndexer> _indexers)
{
    internal void Apply<T>(IndexedModelCollection<T> collection) where T : IModelWithMetadata
    {
        foreach (var indexer in _indexers)
        {
            collection.CreateIndex(indexer);
        }
    }
}
