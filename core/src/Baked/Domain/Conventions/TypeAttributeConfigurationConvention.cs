using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class TypeAttributeConfigurationConvention<TAttribute>(Action<TAttribute, TypeModelMetadataContext> apply, Order order,
    Func<TypeModelMetadataContext, TAttribute, bool>? when = default
) : AttributeConfigurationConventionBase<TypeModelMetadataContext, TAttribute>(apply, order, when: when)
    where TAttribute : Attribute
{
    protected override ICustomAttributesModel GetMetadata(TypeModelMetadataContext context) =>
        context.Type;
}