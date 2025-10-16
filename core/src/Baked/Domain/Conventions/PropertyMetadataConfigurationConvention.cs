using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class PropertyMetadataConfigurationConvention<TAttribute>(Action<TAttribute, PropertyModelContext> apply,
    Func<PropertyModelContext, TAttribute, bool>? when = default
) : MetadataConfigurationConventionBase<PropertyModelContext, TAttribute>(apply, when: when)
    where TAttribute : Attribute
{
    protected override ICustomAttributesModel GetMetadata(PropertyModelContext context) =>
        context.Property;
}