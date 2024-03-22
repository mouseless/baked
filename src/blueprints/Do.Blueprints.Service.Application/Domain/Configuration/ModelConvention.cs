using Do.Domain.Configuration;
using Do.Domain.Model;

namespace Do.Domain.Convention;

public class ModelConvention<T>(int _order, Func<T, bool> _appliesTo, Action<T> _apply) : IModelConvention<T>
    where T : IModel
{
    ModelConfigurators _configurators = default!;

    internal ModelConvention<T> Initialize(ModelConfigurators configurators)
    {
        _configurators = configurators;

        return this;
    }

    public int Order => _order;

    public bool AppliesTo(T model) => _appliesTo(model);
    public void Apply(T model) => _apply(model);

    IModelConvention<T> IModelConvention<T>.Initialize(ModelConfigurators configurators) => Initialize(configurators);
}

public class ModelConvention<T, TConfigurer>(int _order, Func<T, bool> _appliesTo, Action<T, TConfigurer> _apply) : IModelConvention<T>
    where T : IModel
    where TConfigurer : IModelConfigurer
{
    ModelConfigurators _configurators = default!;

    internal ModelConvention<T, TConfigurer> Initialize(ModelConfigurators configurators)
    {
        _configurators = configurators;

        return this;
    }

    public int Order => _order;

    public bool AppliesTo(T model) => _appliesTo(model);
    public void Apply(T model) => _apply(model, _configurators.Get<TConfigurer>());

    IModelConvention<T> IModelConvention<T>.Initialize(ModelConfigurators configurators) => Initialize(configurators);
}
