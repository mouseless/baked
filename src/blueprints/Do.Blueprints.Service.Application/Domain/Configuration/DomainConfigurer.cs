using Do.Domain.Model;

namespace Do.Domain.Configuration;

internal class DomainConfigurer(BuildDomainContext _buildDomainContext, DomainConventions _conventions)
{
    readonly ModelConventionCollection<TypeModel> _type = _conventions.Type.Initialize(_buildDomainContext);
    readonly ModelConventionCollection<MethodModel> _method = _conventions.Method.Initialize(_buildDomainContext);
    readonly ModelConventionCollection<ParameterModel> _parameter = _conventions.Parameter.Initialize(_buildDomainContext);
    readonly ModelConventionCollection<PropertyModel> _property = _conventions.Property.Initialize(_buildDomainContext);

    internal void Execute(DomainModel model)
    {
        _type.Apply(model.Types);

        foreach (var methods in model.Types.Select(t => t.Methods))
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

        foreach (var properties in model.Types.Select(t => t.Properties))
        {
            _property.Apply(properties);
        }
    }
}