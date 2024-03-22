namespace Do.Domain.Model;

public class AttributeIndexer<T>() : IndexerBase
    where T : Attribute
{
    protected override string IndexId =>
        TypeModel.IdFrom(typeof(T));

    protected override bool CanIndex(IModel model) =>
        model is IModelWithMetadata m && m.CustomAttributes.Contains(IndexId);
}
