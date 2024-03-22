namespace Do.Domain.Model;

public class MethodModelCollection(IEnumerable<MethodModel> models, List<AttributeIndexer> indexers)
    : IndexedModelCollection<MethodModel>(models, indexers), IIndexedModelCollection<MethodModel, MethodModelCollection>
{
    static MethodModelCollection IIndexedModelCollection<MethodModel, MethodModelCollection>.New(IEnumerable<MethodModel> models, List<AttributeIndexer> indexers) =>
        new(models, indexers);

    public MethodModelCollection() : this([], []) { }

    public ModelCollection<MethodModel> GetIndex<T>() where T : class
        => GetIndex(TypeModel.IdFrom(typeof(T)));
}
