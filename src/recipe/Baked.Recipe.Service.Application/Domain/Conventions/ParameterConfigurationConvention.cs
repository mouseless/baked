using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class ParameterConfigurationConvention<TAttribute>(Action<TAttribute, ParameterModelContext> apply,
    Func<TAttribute, ParameterModelContext, bool>? when = default
) : MetadataConfigurationConventionBase<ParameterModelContext, TAttribute>(apply, when: when)
    where TAttribute : Attribute
{
    protected override ICustomAttributesModel GetMetadata(ParameterModelContext context) =>
        context.Parameter;
}