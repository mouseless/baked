using Baked.Domain.Configuration;

namespace Baked.Domain.Conventions;

public class RemoveMetadataFromTypeConvention<TAttribute>(Func<TypeModelMetadataContext, bool> _when,
    int _order = default
) : IDomainModelConvention<TypeModelMetadataContext> where TAttribute : Attribute
{
    public int Order => _order;

    public void Apply(TypeModelMetadataContext model)
    {
        if (!_when(model)) { return; }

        model.Type.CustomAttributes.Remove<TAttribute>();
    }
}