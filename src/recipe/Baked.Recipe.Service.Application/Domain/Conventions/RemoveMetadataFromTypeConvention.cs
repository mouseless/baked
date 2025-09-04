using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class RemoveMetadataFromTypeConvention<TAttribute>(Func<TypeModelMetadataContext, bool> _when)
    : IDomainModelConvention<TypeModelMetadataContext>, IAddRemoveMetadataConvention where TAttribute : Attribute
{
    public void Apply(TypeModelMetadataContext model)
    {
        if (!_when(model)) { return; }

        ((IMutableAttributeCollection)model.Type.CustomAttributes).Remove<TAttribute>();
    }
}