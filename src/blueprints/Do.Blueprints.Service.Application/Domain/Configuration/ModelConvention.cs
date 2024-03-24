using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class ModelConvention<T, TComponent>(int _order, Func<T, bool> _appliesTo, Action<T, TComponent> _apply) : IModelConvention
    where T : IModel
    where TComponent : class, IDomainService
{
    DomainServiceProvider _sp = default!;

    void Initialize(DomainServiceProvider buildDomainContext)
    {
        _sp = buildDomainContext;
    }

    int IModelConvention.Order => _order;
    bool IModelConvention.AppliesTo(IModel model) => _appliesTo((T)model);
    void IModelConvention.Apply(IModel model) => _apply((T)model, _sp.Get<TComponent>());

    void IModelConvention.Initialize(DomainServiceProvider sp) => Initialize(sp);
}
