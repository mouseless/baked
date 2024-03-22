namespace Do.Domain.Model;

public class DomainConventionProcessor
{
    public ModelConventionCollection<TypeModel> Type { get; } = [];
    public ModelConventionCollection<PropertyModel> Property { get; } = [];
    public ModelConventionCollection<MethodModel> Method { get; } = [];

    public void Execute(DomainModel domainModel)
    {
        Type.Apply(domainModel.Types);

        foreach (var methods in domainModel.Types.Select(t => t.Methods))
        {
            Method.Apply(methods);
        }

        foreach (var properties in domainModel.Types.Select(t => t.Properties))
        {
            Property.Apply(properties);
        }
    }
}
