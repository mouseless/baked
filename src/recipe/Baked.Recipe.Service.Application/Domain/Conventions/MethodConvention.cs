using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class MethodConvention<TAttribute>(Func<MethodModelContext, bool> when, Action<TAttribute, MethodModelContext> apply)
    : MetadataConventionBase<MethodModelContext, TAttribute>(when, apply)
      where TAttribute : Attribute
{
    public MethodConvention(Func<MethodModelContext, bool> when, Action<TAttribute> apply)
        : this(when, (d, c) => apply(d)) { }

    protected override ICustomAttributesModel GetMetadata(MethodModelContext context) =>
        context.Method;
}