using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class PropertyConvention<TAttribute>(Func<PropertyModelContext, bool> when, Action<TAttribute, PropertyModelContext> apply)
    : MetadataConventionBase<PropertyModelContext, TAttribute>(when, apply)
      where TAttribute : Attribute
{
    public PropertyConvention(Func<PropertyModelContext, bool> when, Action<TAttribute> apply)
        : this(when, (d, c) => apply(d)) { }

    protected override ICustomAttributesModel GetMetadata(PropertyModelContext context) =>
        context.Property;
}