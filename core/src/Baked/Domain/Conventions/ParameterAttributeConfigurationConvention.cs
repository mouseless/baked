using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class ParameterAttributeConfigurationConvention<TAttribute>(Action<TAttribute, ParameterModelContext> apply, Order order,
    Func<ParameterModelContext, TAttribute, bool>? when = default
) : AttributeConfigurationConventionBase<ParameterModelContext, TAttribute>(apply, order, when: when)
    where TAttribute : Attribute
{
    protected override ICustomAttributesModel GetMetadata(ParameterModelContext context) =>
        context.Parameter;
}