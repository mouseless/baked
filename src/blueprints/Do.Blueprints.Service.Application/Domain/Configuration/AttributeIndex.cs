using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class AttributeIndex : ModelCollectionIndexBase
{
    public static AttributeIndex New<T>() where T : Attribute
            => new AttributeIndex().With(typeof(T));
    public static AttributeIndex New(Attribute attribute)
        => new AttributeIndex().With(attribute.GetType(), attribute);

    ModelIndexKey _modelIndexKey = default!;
    Func<IMemberModel, bool> _predicateFunction = default!;

    protected AttributeIndex() { }

    AttributeIndex With(Type attributeType,
        Attribute? attribute = default
    )
    {
        _modelIndexKey = attribute is not null ? new(attribute) : new(attributeType);
        _predicateFunction = attribute is not null ? model => model.CustomAttributes.Contains(attribute) : model => model.CustomAttributes.ContainsKey(attributeType);

        return this;
    }

    protected override ModelIndexKey IndexKey =>
        _modelIndexKey;

    protected override bool Selector(IModel model) =>
        model is IMemberModel member && _predicateFunction(member);
}
