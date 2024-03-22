namespace Do.Domain.Model;

public class TypeModelCollection(IEnumerable<TypeModel> models, List<AttributeIndexer> indexer)
    : IndexedModelCollection<TypeModel>(models, indexer), IIndexedModelCollection<TypeModel, TypeModelCollection>
{
    static TypeModelCollection IIndexedModelCollection<TypeModel, TypeModelCollection>.New(IEnumerable<TypeModel> models, List<AttributeIndexer> _indexers)
        => new(models, _indexers);

    public TypeModel this[Type type] =>
        this[TypeModel.IdFrom(type)];

    public bool Contains(Type type) =>
        Contains(TypeModel.IdFrom(type));

    public ModelCollection<TypeModel> GetIndex<T>() where T : class
        => GetIndex(TypeModel.IdFrom(typeof(T)));
}
