using Baked.Domain.Configuration;

namespace Baked.Domain.Conventions;

public class RemoveMetadataFromTypeConvention<TAttribute>(Func<TypeModelMetadataContext, bool> _when)
    : IDomainModelConvention<TypeModelMetadataContext>, IMetadataConvention where TAttribute : Attribute
{
    public void Apply(TypeModelMetadataContext model)
    {
        if (!_when(model)) { return; }

        model.Type.CustomAttributes.Remove<TAttribute>();
    }
}