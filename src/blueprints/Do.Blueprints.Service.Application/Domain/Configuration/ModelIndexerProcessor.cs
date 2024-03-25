using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class ModelIndexerProcessor<T>(DomainIndexerCollection _indexers) : IModelCollectionConfigurer<T>
     where T : IModel
{
    void Apply(ModelCollection<T> collection)
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

    void IModelCollectionConfigurer<T>.Apply(ModelCollection<T> collection) => Apply(collection);
}

