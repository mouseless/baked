using Do.Domain.Model;

namespace Do.Domain.Configuration;

internal class DomainConfigurer(DomainConventions _conventions)
{
    internal void Execute(DomainModel model)
    {
        _conventions.Type.Apply(model.Types);

        foreach (var methods in model.Types.Select(t => t.Methods))
        {
            _conventions.Method.Apply(methods);

            foreach (var overloads in methods.Select(m => m.Overloads))
            {
                foreach (var overload in overloads)
                {
                    _conventions.Parameter.Apply(overload.Parameters);
                }
            }
        }

        foreach (var properties in model.Types.Select(t => t.Properties))
        {
            _conventions.Property.Apply(properties);
        }
    }
}