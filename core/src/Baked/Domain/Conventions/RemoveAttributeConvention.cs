using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class RemoveAttributeConvention<TModelContext, TAttribute>(
    Action<TModelContext, Action<ICustomAttributesModel>> _apply,
    Func<TModelContext, bool> _when,
    bool attributeRequiresIndex = true
) : IDomainModelConvention<TModelContext>, IAddRemoveAttributeConvention where TAttribute : Attribute
{
    bool IAddRemoveAttributeConvention.AttributeRequiresIndex => attributeRequiresIndex;

    public void Apply(TModelContext model)
    {
        if (!_when(model)) { return; }

        _apply(model, Remove);
    }

    void Remove(ICustomAttributesModel model) =>
        ((IMutableAttributeCollection)model.CustomAttributes).Remove<TAttribute>();
}