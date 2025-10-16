using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class ParameterMetadataConfigurationConvention<TAttribute>(Action<TAttribute, ParameterModelContext> apply,
    Func<ParameterModelContext, TAttribute, bool>? when = default
) : MetadataConfigurationConventionBase<ParameterModelContext, TAttribute>(apply, when: when)
    where TAttribute : Attribute
{
    protected override ICustomAttributesModel GetMetadata(ParameterModelContext context) =>
        context.Parameter;
}