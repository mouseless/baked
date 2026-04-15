using Baked.Domain.Model;

namespace Baked.Domain.Export;

public class AttributeExport<T> : IAttributeExport where T : Attribute
{
    List<Func<T, ICustomAttributesModel, bool>> _filters = [];

    internal List<Func<T, AttributeProperty>> Properties { get; } = [];
    internal List<Func<AttributeProperty, bool>> RemoveProperty { get; } = [];

    public AttributeExport<T> AddFilter(Func<T, ICustomAttributesModel, bool> condition)
    {
        _filters.Add(condition);

        return this;
    }

    public void AddProperty(Func<T, AttributeProperty> property) =>
        Properties.Add(property);

    public void ExcludeProperty(
        Func<AttributeProperty, bool>? filter = default
    ) => RemoveProperty.Add(filter ?? (_ => true));

    Type IAttributeExport.Type => typeof(T);
    List<Func<Attribute, AttributeProperty>> IAttributeExport.Properties =>
         [.. Properties.Select(property => (Func<Attribute, AttributeProperty>)(attr => property((T)attr)))];
    List<Func<AttributeProperty, bool>> IAttributeExport.RemoveProperty =>
        RemoveProperty;

    bool IAttributeExport.AppliesTo(Attribute instance, ICustomAttributesModel model)
    {
        if (!_filters.Any()) { return true; }

        return _filters.Any(c => c((T)instance, model));
    }
}