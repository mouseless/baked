using Baked.Business;
using Baked.Domain.Model;

namespace Baked.Domain.Export;

public class ExportAttribute<T> : IAttributeExport where T : Attribute
{
    List<Func<T, ICustomAttributesModel, bool>> _filters = [];

    internal List<Func<T, AttributeProperty>> PropertyExtensions { get; } = [];
    internal List<Func<AttributeProperty, bool>> RemoveProperty { get; } = [];

    public ExportAttribute<T> AddFilter(Func<T, ICustomAttributesModel, bool> condition)
    {
        _filters.Add(condition);

        return this;
    }

    public void AddPropertyExtension(Func<T, AttributeProperty> property) =>
        PropertyExtensions.Add(property);

    public void ExcludeProperty(
        Func<AttributeProperty, bool>? filter = default
    ) => RemoveProperty.Add(filter ?? (_ => true));

    Type IAttributeExport.Type => typeof(T);
    List<Func<Attribute, AttributeProperty>> IAttributeExport.PropertyExtensions =>
         [.. PropertyExtensions.Select(extension => (Func<Attribute, AttributeProperty>)(attr => extension((T)attr)))];
    List<Func<AttributeProperty, bool>> IAttributeExport.RemoveProperty =>
        RemoveProperty;

    bool IAttributeExport.AppliesTo(Attribute instance, ICustomAttributesModel model)
    {
        if (!_filters.Any()) { return true; }

        return _filters.Any(c => c((T)instance, model));
    }
}