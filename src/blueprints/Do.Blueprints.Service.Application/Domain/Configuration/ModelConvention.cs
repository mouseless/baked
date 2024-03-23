using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class ModelConvention<T>(int _order, Func<T, bool> _appliesTo, Action<T> _apply) : IModelConvention<T>
    where T : IModel
{
    int IModelConvention<T>.Order => _order;
    bool IModelConvention<T>.AppliesTo(T model) => _appliesTo(model);
    void IModelConvention<T>.Apply(T model) => _apply(model);

    void IModelConvention<T>.Initialize(BuildDomainContext _) { }
}

public class ModelConvention<T, TComponent>(int _order, Func<T, bool> _appliesTo, Action<T, TComponent> _apply) : IModelConvention<T>
    where T : IModel
    where TComponent : class, IDomainComponent
{
    BuildDomainContext _domainBuilderContext = default!;

    void Initialize(BuildDomainContext domainBuilderContext)
    {
        _domainBuilderContext = domainBuilderContext;
    }

    int IModelConvention<T>.Order => _order;
    bool IModelConvention<T>.AppliesTo(T model) => _appliesTo(model);
    void IModelConvention<T>.Apply(T model) => _apply(model, _domainBuilderContext.Get<TComponent>());

    void IModelConvention<T>.Initialize(BuildDomainContext domainBuilderContext) => Initialize(domainBuilderContext);
}
