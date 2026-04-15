namespace Baked.Domain.Export;

public interface IAttributePropertyBuilder
{
    List<AttributeProperty> Build(object instance);
}