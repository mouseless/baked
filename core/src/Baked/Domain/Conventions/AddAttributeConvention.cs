using Baked.Domain.Configuration;
using Baked.Domain.Model;
using Baked.Theme;

namespace Baked.Domain.Conventions;

public class AddAttributeConvention<TModelContext>(
    Action<TModelContext, Action<ICustomAttributesModel, Attribute>> _apply,
    Func<TModelContext, bool> _when,
    bool attributeRequiresIndex = true
) : IDomainModelConvention<TModelContext>, IAddRemoveAttributeConvention
    where TModelContext : DomainModelContext
{
    readonly Inspection _inspect = Inspect.TraceHere();

    bool IAddRemoveAttributeConvention.AttributeRequiresIndex => attributeRequiresIndex;

    public void Apply(TModelContext context)
    {
        var old = context.Inspect;
        context.Inspect = _inspect;

        try
        {
            if (!_when(context)) { return; }

            _apply(context, Add);
        }
        finally { context.Inspect = old; }
    }

    void Add(ICustomAttributesModel model, Attribute attribute)
    {
        attribute.ThrowIfNotTarget(model);

        ((IMutableAttributeCollection)model.CustomAttributes).Add(attribute);
    }
}