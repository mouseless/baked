namespace Do.Domain.Model;

public class DomainMetadataProcessor
{
    public ModelConventionCollection<TypeModel> Type { get; } = [];
    public ModelConventionCollection<MethodModel> Method { get; } = [];

    public void Execute(DomainModel domainModel)
    {
        foreach (var type in domainModel.Types)
        {
            Type.Apply(type, domainModel.TypeCache);

            foreach (var method in type.Methods)
            {
                Method.Apply(method, type.MethodCache);
            }
        }
    }
}
