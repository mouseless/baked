using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class EditParameterAttributeConvention<TAttribute>(Action<TAttribute, ParameterModelContext> apply, Order order,
    Func<ParameterModelContext, TAttribute, bool>? when = default
) : EditAttributeConventionBase<ParameterModelContext, TAttribute>(apply, order, when: when)
    where TAttribute : Attribute
{
    protected override ICustomAttributesModel GetMetadata(ParameterModelContext context) =>
        context.Parameter;
}