using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class ParameterConvention<TAttribute>(Func<ParameterModelContext, bool> when, Action<TAttribute, ParameterModelContext> apply)
    : MetadataConventionBase<ParameterModelContext, TAttribute>(when, apply)
      where TAttribute : Attribute
{
    public ParameterConvention(Func<MethodModelContext, bool> when, Action<TAttribute> apply)
        : this(when, (d, c) => apply(d)) { }

    protected override ICustomAttributesModel GetMetadata(ParameterModelContext context) =>
        context.Method;
}