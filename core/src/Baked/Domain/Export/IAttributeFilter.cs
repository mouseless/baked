using Baked.Business;

namespace Baked.Domain.Export;

public interface IAttributeFilter
{
    public Type Type { get; }

    public List<Func<Attribute, AttributeProperty>> PropertyExtensions { get; }
    public List<Func<AttributeProperty, bool>> RemoveProperty { get; }
}