namespace Do.Domain.Model;

internal class DomainConventionProcessor(DomainConventions _domainConventions)
{
    internal void Execute(ModelCollection<TypeModel> types)
    {
        _domainConventions.Type.Apply(types);

        foreach (var methods in types.Select(t => t.Methods))
        {
            _domainConventions.Method.Apply(methods);
        }
    }
}