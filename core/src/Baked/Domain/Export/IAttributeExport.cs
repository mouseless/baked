using Baked.Domain.Model;

namespace Baked.Domain.Export;

public interface IAttributeExport
{
    Type Type { get; }
    List<Func<Attribute, AttributeProperty>> Properties { get; }
    List<Func<AttributeProperty, bool>> RemoveProperty { get; }

    bool AppliesTo(Attribute instance, ICustomAttributesModel model);
}