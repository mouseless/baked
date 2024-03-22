using Do.Domain.Convention;
using Do.Domain.Model;

namespace Do.Domain.Configuration;

internal class DomainConventionProcessor(ModelConfigurators _configurators)
{
    ModelConventionCollection<TypeModel> _type = default!;
    ModelConventionCollection<MethodModel> _method = default!;
    ModelConventionCollection<ParameterModel> _parameter = default!;
    ModelConventionCollection<PropertyModel> _property = default!;

    internal DomainConventionProcessor With(DomainConventions conventions)
    {
        _type = conventions.Type.Initialize(_configurators);
        _method = conventions.Method.Initialize(_configurators);
        _parameter = conventions.Parameter.Initialize(_configurators);
        _property = conventions.Property.Initialize(_configurators);

        return this;
    }

    internal void Execute(ModelCollection<TypeModel> types)
    {
        _type.Apply(types);

        foreach (var methods in types.Select(t => t.Methods))
        {
            _method.Apply(methods);

            foreach (var overloads in methods.Select(m => m.Overloads))
            {
                foreach (var overload in overloads)
                {
                    _parameter.Apply(overload.Parameters);
                }
            }
        }

        foreach (var properties in types.Select(t => t.Properties))
        {
            _property.Apply(properties);
        }
    }
}