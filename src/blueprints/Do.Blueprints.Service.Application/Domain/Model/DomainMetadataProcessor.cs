namespace Do.Domain.Model;

public class DomainMetadataProcessor
{
    public ModelConventionCollection<TypeModel> Type { get; } = [];
    public ModelConventionCollection<MethodModel> Method { get; } = [];

    public void Execute(KeyedModelCollection<TypeModel> types)
    {
        foreach (var type in types)
        {
            Type.Apply(type);

            foreach (var method in type.Methods)
            {
                Method.Apply(method);
            }
        }
    }
}
