using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class ParameterConvention<TAttribute>(Action<TAttribute, ParameterModelContext> apply,
    Func<TAttribute, ParameterModelContext, bool>? when = default
) : MetadataConventionBase<ParameterModelContext, TAttribute>(apply, when: when)
    where TAttribute : Attribute
{
    protected override ICustomAttributesModel GetMetadata(ParameterModelContext context) =>
        context.Parameter;
}