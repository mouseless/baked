using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class ModelIndexerProcessor(ModelIndexerCollection _indexers) : IDomainService
{
    static IDomainService IDomainService.New(DomainServiceProvider sp) =>
        new ModelIndexerProcessor(sp.Get<ModelIndexerCollection>());

    internal void Apply<T>(ModelCollection<T> collection) where T : IModel
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

