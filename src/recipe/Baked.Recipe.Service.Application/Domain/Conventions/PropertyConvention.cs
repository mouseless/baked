using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class PropertyConvention<TAttribute>(Action<TAttribute, PropertyModelContext> apply,
    Func<TAttribute, PropertyModelContext, bool>? when = default
) : MetadataConventionBase<PropertyModelContext, TAttribute>(apply, when: when)
    where TAttribute : Attribute
{
    protected override ICustomAttributesModel GetMetadata(PropertyModelContext context) =>
        context.Property;
}