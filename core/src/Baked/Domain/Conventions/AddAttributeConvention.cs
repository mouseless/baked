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
    readonly InspectTrace _trace = Inspect.TraceHere();

    bool IAddRemoveAttributeConvention.AttributeRequiresIndex => attributeRequiresIndex;

    public void Apply(TModelContext context)
    {
        context.Trace = _trace;

        if (!_when(context)) { return; }

        _apply(context, (model, attribute) =>
            _trace.Capture(context, () =>
            {
                Add(model, attribute);

                return attribute;
            })
        );
    }

    static void Add(ICustomAttributesModel model, Attribute attribute)
    {
        attribute.ThrowIfNotTarget(model);

        ((IMutableAttributeCollection)model.CustomAttributes).Add(attribute);
    }
}