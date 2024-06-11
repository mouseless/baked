using Baked.Domain.Configuration;
using Baked.Domain.Model;

namespace Baked.Domain.Conventions;

public class MetadataConvention<TModelContext>(
    Action<TModelContext, Action<ICustomAttributesModel, Attribute>> _apply,
    Func<TModelContext, bool> _when,
    int _order = default
) : IDomainModelConvention<TModelContext>
{
    public int Order => _order;

    public void Apply(TModelContext model)
    {
        if (_when(model))
        {
            _apply(model, Add);
        }
    }

    void Add(ICustomAttributesModel model, Attribute attribute)
    {
        model.CustomAttributes.Add(attribute);
    }
}