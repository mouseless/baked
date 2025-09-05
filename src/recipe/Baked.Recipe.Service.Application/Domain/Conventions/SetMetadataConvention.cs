using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class SetMetadataConvention<TModelContext>(
    Action<TModelContext, Action<ICustomAttributesModel, Attribute>> _apply,
    Func<TModelContext, bool> _when
) : IDomainModelConvention<TModelContext>, IAddRemoveMetadataConvention
{
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