using Baked.Business;
using Baked.Domain.Model;

namespace Baked.Domain.Export;

public interface IAttributeExport
{
    public Type Type { get; }
    public List<Func<Attribute, AttributeProperty>> PropertyExtensions { get; }
    public List<Func<AttributeProperty, bool>> RemoveProperty { get; }

    public bool AppliesTo(Attribute instance, ICustomAttributesModel model);
}