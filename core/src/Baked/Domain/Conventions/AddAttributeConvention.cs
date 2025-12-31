using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class AddAttributeConvention<TModelContext>(
    Action<TModelContext, Action<ICustomAttributesModel, Attribute>> _apply,
    Func<TModelContext, bool> _when,
    bool attributeRequiresIndex = true
) : IDomainModelConvention<TModelContext>, IAddRemoveAttributeConvention
{
    bool IAddRemoveAttributeConvention.AttributeRequiresIndex => attributeRequiresIndex;

    public void Apply(TModelContext model)
    {
        if (!_when(model)) { return; }

        _apply(model, Add);
    }

    void Add(ICustomAttributesModel model, Attribute attribute) =>
        ((IMutableAttributeCollection)model.CustomAttributes).Add(attribute);
}