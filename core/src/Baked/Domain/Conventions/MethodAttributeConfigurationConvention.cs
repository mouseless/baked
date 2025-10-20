using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class MethodAttributeConfigurationConvention<TAttribute>(Action<TAttribute, MethodModelContext> apply,
    Func<MethodModelContext, TAttribute, bool>? when = default
) : AttributeConfigurationConventionBase<MethodModelContext, TAttribute>(apply, when: when)
    where TAttribute : Attribute
{
    protected override ICustomAttributesModel GetMetadata(MethodModelContext context) =>
        context.Method;
}