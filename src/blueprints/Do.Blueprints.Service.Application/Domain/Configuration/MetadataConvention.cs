using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class MetadataConvention<TModel>(
    Action<TModel, Action<ICustomAttributesModel, Attribute>> _apply,
    Func<TModel, bool> _when,
    int? _order = default
) : IDomainModelConvention<TModel> where TModel : IModel
{
    public int Order => _order ?? 0;

    public void Apply(TModel model)
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