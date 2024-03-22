namespace Do.Domain.Model;

public class IndexedModelCollection<T> : ModelCollection<T> where T : IModelWithMetadata
{
    readonly ModelIndex<T> _index = [];

    internal IndexedModelCollection() { }

    internal IndexedModelCollection(IEnumerable<T> models)
        : base()
    {
        foreach (var model in models)
        {
            Models.Add(model);
        }
    }

    public ModelCollection<T> GetIndex(string id) =>
        _index.GetOrEmpty(id);

    internal void CreateIndex(AttributeIndexer indexer)
    {
        foreach (var model in Models)
        {
            indexer.Execute(_index, model);
        }
    }
}