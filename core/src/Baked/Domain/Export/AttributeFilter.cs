using Baked.Business;

namespace Baked.Domain.Export;

public class AttributeFilter<T> : IAttributeFilter where T : Attribute
{
    internal List<Func<T, AttributeProperty>> PropertyExtensions { get; } = [];
    internal List<Func<AttributeProperty, bool>> RemoveProperty { get; } = [];

    public void AddData(Func<T, AttributeProperty> property) =>
        PropertyExtensions.Add(property);

    public void ExcludeData(
        Func<AttributeProperty, bool>? filter = default
    ) => RemoveProperty.Add(filter ?? (_ => true));

    Type IAttributeFilter.Type => typeof(T);

    List<Func<Attribute, AttributeProperty>> IAttributeFilter.PropertyExtensions =>
         [.. PropertyExtensions.Select(extension => (Func<Attribute, AttributeProperty>)(attr => extension((T)attr)))];
    List<Func<AttributeProperty, bool>> IAttributeFilter.RemoveProperty =>
        RemoveProperty;
}