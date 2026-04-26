using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.Theme;

namespace Baked.Domain.Conventions;

public abstract class AttributeConfigurationConventionBase<TModelContext, TAttribute>(Action<TAttribute, TModelContext> apply,
    Func<TModelContext, TAttribute, bool>? when = default
) : IDomainModelConvention<TModelContext>
    where TAttribute : Attribute
    where TModelContext : DomainModelContext
{
    readonly InspectTrace _trace = Inspect.TraceHere();

    protected abstract ICustomAttributesModel GetMetadata(TModelContext context);

    public void Apply(TModelContext context)
    {
        context.Trace = _trace;

        var attributes = new List<TAttribute>();
        if (typeof(TAttribute).AllowsMultiple())
        {
            if (GetMetadata(context).TryGetAll<TAttribute>(out var list))
            {
                attributes.AddRange(list);
            }
        }
        else if (GetMetadata(context).TryGet<TAttribute>(out var single))
        {
            attributes.Add(single);
        }

        foreach (var attribute in attributes)
        {
            if (when is not null && !when(context, attribute)) { continue; }

            apply(attribute, context);
        }
    }
}