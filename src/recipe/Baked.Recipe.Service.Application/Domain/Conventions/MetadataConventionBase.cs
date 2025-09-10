using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public abstract class MetadataConventionBase<TModelContext, TAttribute>(Func<TModelContext, bool> when, Action<TAttribute, TModelContext> apply)
    : IDomainModelConvention<TModelContext>
      where TAttribute : Attribute
{
    protected abstract ICustomAttributesModel GetMetadata(TModelContext context);

    public void Apply(TModelContext context)
    {
        if (!when(context)) { return; }

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
            apply(attribute, context);
        }
    }
}