using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class SetAttributeConvention<TModelContext>(
    Action<TModelContext, Action<ICustomAttributesModel, Attribute>> _apply,
    Func<TModelContext, bool> _when,
    bool attributeRequiredIndex = true
) : IDomainModelConvention<TModelContext>, IAddRemoveAttributeConvention
{
    bool IAddRemoveAttributeConvention.AttributeRequiresIndex => attributeRequiredIndex;

    public void Apply(TModelContext model)
    {
        if (_when(model))
        {
            _apply(model, Set);
        }
    }

    void Set(ICustomAttributesModel model, Attribute attribute) =>
        ((IMutableAttributeCollection)model.CustomAttributes).Set(attribute);
}