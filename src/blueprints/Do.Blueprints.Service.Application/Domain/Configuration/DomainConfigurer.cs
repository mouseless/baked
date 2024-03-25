using Do.Domain.Model;

namespace Do.Domain.Configuration;

internal class DomainConfigurer(IDomainConfiguration _configuration)
{
    internal void Execute(DomainModel model)
    {
        _configuration.Type.Apply(model.Types);

        foreach (var methods in model.Types.Select(t => t.Methods))
        {
            _configuration.Method.Apply(methods);

            foreach (var overloads in methods.Select(m => m.Overloads))
            {
                foreach (var overload in overloads)
                {
                    _configuration.Parameter.Apply(overload.Parameters);
                }
            }
        }

        foreach (var properties in model.Types.Select(t => t.Properties))
        {
            _configuration.Property.Apply(properties);
        }
    }
}

public interface IDomainConfiguration
{
    IModelCollectionConfigurer<TypeModel> Type { get; }
    IModelCollectionConfigurer<MethodModel> Method { get; }
    IModelCollectionConfigurer<ParameterModel> Parameter { get; }
    IModelCollectionConfigurer<PropertyModel> Property { get; }
}

public interface IModelCollectionConfigurer<T> where T : IModel
{
    void Apply(ModelCollection<T> collection);
}