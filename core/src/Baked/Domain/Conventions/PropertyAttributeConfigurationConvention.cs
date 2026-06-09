using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class PropertyAttributeConfigurationConvention<TAttribute>(Action<TAttribute, PropertyModelContext> apply,
    Order order,
    Func<PropertyModelContext, TAttribute, bool>? when = default
) : AttributeConfigurationConventionBase<PropertyModelContext, TAttribute>(apply, order, when: when)
    where TAttribute : Attribute
{
    protected override ICustomAttributesModel GetMetadata(PropertyModelContext context) =>
        context.Property;
}