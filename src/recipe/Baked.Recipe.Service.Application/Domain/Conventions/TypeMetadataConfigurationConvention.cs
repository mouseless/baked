using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class TypeMetadataConfigurationConvention<TAttribute>(Action<TAttribute, TypeModelMetadataContext> apply,
    Func<TAttribute, TypeModelMetadataContext, bool>? when = default
) : MetadataConfigurationConventionBase<TypeModelMetadataContext, TAttribute>(apply, when: when)
    where TAttribute : Attribute
{
    protected override ICustomAttributesModel GetMetadata(TypeModelMetadataContext context) =>
        context.Type;
}