namespace Do.Domain.Model;

internal class DomainConventionProcessor(DomainConventions _domainConventions)
{
    internal void Execute(ModelCollection<TypeModel> types)
    {
        _domainConventions.Type.Apply(types);

        foreach (var methods in types.Select(t => t.Methods))
        {
            _domainConventions.Method.Apply(methods);

            foreach (var overloads in methods.Select(m => m.Overloads))
            {
                foreach (var overload in overloads)
                {
                    _domainConventions.Parameter.Apply(overload.Parameters);
                }
            }
        }

        foreach (var properties in types.Select(t => t.Properties))
        {
            _domainConventions.Property.Apply(properties);
        }
    }
}