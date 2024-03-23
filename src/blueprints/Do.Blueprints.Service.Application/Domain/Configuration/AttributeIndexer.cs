using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class AttributeIndexer<T>() : IndexerBase
    where T : Attribute
{
    protected override string IndexId =>
        TypeModel.IdFrom(typeof(T));

    protected override bool AppliesTo(IModel model) =>
        model is IModelWithMetadata m && m.CustomAttributes.Contains(IndexId);
}
