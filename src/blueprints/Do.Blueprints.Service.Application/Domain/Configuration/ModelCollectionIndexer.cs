using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class ModelCollectionIndexer<T>(List<IModelCollectionIndex> _indexList) : IModelCollectionConfigurer<T>
     where T : IModel
{
    void Apply(IModelCollectionWithIndex<T> collection)
    {
        foreach (var index in _indexList)
        {
            collection.Index[index.IndexKey] = new([.. collection.Where(i => index.Selector(i))]);
        }
    }

    void IModelCollectionConfigurer<T>.Apply(ModelCollection<T> collection) => Apply(collection);
}

