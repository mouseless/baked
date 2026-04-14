using Baked.Business;

namespace Baked.Domain.Export;

public interface IAttributeDataBuilder
{
    List<AttributeProperty> Build(object instance);
}