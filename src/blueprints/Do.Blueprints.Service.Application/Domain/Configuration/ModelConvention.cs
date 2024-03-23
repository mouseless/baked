using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class ModelConvention<T>(int _order, Func<T, bool> _appliesTo, Action<T> _apply) : IModelConvention<T>
    where T : IModel
{
    int IModelConvention<T>.Order => _order;
    bool IModelConvention<T>.AppliesTo(T model) => _appliesTo(model);
    void IModelConvention<T>.Apply(T model) => _apply(model);

    void IModelConvention<T>.Initialize(DomainBuilderContext _) { }
}

public class ModelConvention<T, TComponent>(int _order, Func<T, bool> _appliesTo, Action<T, TComponent> _apply) : IModelConvention<T>
    where T : IModel
    where TComponent : class, IDomainComponent
{
    DomainBuilderContext _domainBuilderContext = default!;

    void Initialize(DomainBuilderContext domainBuilderContext)
    {
        _domainBuilderContext = domainBuilderContext;
    }

    int IModelConvention<T>.Order => _order;
    bool IModelConvention<T>.AppliesTo(T model) => _appliesTo(model);
    void IModelConvention<T>.Apply(T model) => _apply(model, _domainBuilderContext.Get<TComponent>());

    void IModelConvention<T>.Initialize(DomainBuilderContext domainBuilderContext) => Initialize(domainBuilderContext);
}
