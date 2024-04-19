using Do.Domain.Configuration;
using Do.Domain.Model;

namespace Do.Domain.Conventions;

public class MetadataConvention<TModelContext>(
    Action<TModelContext, Action<ICustomAttributesModel, Attribute>> _apply,
    Func<TModelContext, bool> _when,
    int? _order = default
) : IDomainModelConvention<TModelContext>
{
    public int Order => _order ?? 0;

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