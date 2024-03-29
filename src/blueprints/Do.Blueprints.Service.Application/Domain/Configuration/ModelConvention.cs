using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class ModelConvention<TModel, TComponent>(Func<TModel, bool> _when, Action<TModel, TComponent> _apply,
    int? _order = default
) : IModelConvention<TModel>
    where TModel : IModel
    where TComponent : IConventionComponent<TComponent>
{
    readonly TComponent _component = TComponent.New();

    int IModelConvention<TModel>.Order => _order ?? 0;

    void IModelConvention<TModel>.Apply(TModel model)
    {
        if (_when(model))
        {
            _apply(model, _component);
        }
    }
}
