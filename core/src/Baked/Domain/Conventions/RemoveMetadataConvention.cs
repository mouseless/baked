using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class RemoveMetadataConvention<TModelContext, TAttribute>(
    Action<TModelContext, Action<ICustomAttributesModel>> _apply,
    Func<TModelContext, bool> _when
) : IDomainModelConvention<TModelContext>, IAddRemoveMetadataConvention where TAttribute : Attribute
{
    public void Apply(TModelContext model)
    {
        if (!_when(model)) { return; }

        _apply(model, Remove);
    }

    void Remove(ICustomAttributesModel model) =>
        ((IMutableAttributeCollection)model.CustomAttributes).Remove<TAttribute>();
}