using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class TypeConvention<TAttribute>(Func<TypeModelMetadataContext, bool> when, Action<TAttribute, TypeModelMetadataContext> apply)
    : MetadataConventionBase<TypeModelMetadataContext, TAttribute>(when, apply)
      where TAttribute : Attribute
{
    public TypeConvention(Func<TypeModelMetadataContext, bool> when, Action<TAttribute> apply)
        : this(when, (d, c) => apply(d)) { }

    protected override ICustomAttributesModel GetMetadata(TypeModelMetadataContext context) =>
        context.Type;
}