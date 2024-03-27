using Do.Domain.Model;

namespace Do.Domain.Configuration;

internal class DomainConfigurer(IDomainConfiguration _configuration)
{
    internal void Execute(DomainModel model)
    {
        _configuration.Type.Apply(model.ReflectedTypes);

        foreach (var methods in model.ReflectedTypes.Select(t => t.MethodGroups))
        {
            _configuration.MethodGroup.Apply(methods);

            foreach (var overloads in methods.Select(m => m.Methods))
            {
                foreach (var overload in overloads)
                {
                    _configuration.Parameter.Apply(overload.Parameters);
                }
            }
        }

        foreach (var properties in model.ReflectedTypes.Select(t => t.Properties))
        {
            _configuration.Property.Apply(properties);
        }
    }
}
