using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class ModelConvention<T, TComponent>(int _order, Func<T, bool> _appliesTo, Action<T, TComponent> _apply) : IModelConvention<T>
    where T : IModel
    where TComponent : class, IDomainComponent
{
    BuildDomainContext _buildDomainContext = default!;

    void Initialize(BuildDomainContext buildDomainContext)
    {
        _buildDomainContext = buildDomainContext;
    }

    int IModelConvention<T>.Order => _order;
    bool IModelConvention<T>.AppliesTo(T model) => _appliesTo(model);
    void IModelConvention<T>.Apply(T model) => _apply(model, _buildDomainContext.Get<TComponent>());

    void IModelConvention<T>.Initialize(BuildDomainContext buildDomainContext) => Initialize(buildDomainContext);
}
