using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class EditMethodAttributeConvention<TAttribute>(Action<TAttribute, MethodModelContext> apply, Order order,
    Func<MethodModelContext, TAttribute, bool>? when = default
) : EditAttributeConventionBase<MethodModelContext, TAttribute>(apply, order, when: when)
    where TAttribute : Attribute
{
    protected override ICustomAttributesModel GetMetadata(MethodModelContext context) =>
        context.Method;
}