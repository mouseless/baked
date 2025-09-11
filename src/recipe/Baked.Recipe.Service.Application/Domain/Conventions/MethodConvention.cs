using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class MethodConvention<TAttribute>(Action<TAttribute, MethodModelContext> apply,
    Func<TAttribute, MethodModelContext, bool>? when = default
) : MetadataConventionBase<MethodModelContext, TAttribute>(apply, when: when)
    where TAttribute : Attribute
{
    protected override ICustomAttributesModel GetMetadata(MethodModelContext context) =>
        context.Method;
}