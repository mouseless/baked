namespace DomainModelOverReflection.Models;

public class DomainModel
{
    public List<TypeModel> TypeModels { get; } = new();

    public DomainModel(List<TypeModel> typeModels)
    {
        TypeModels = typeModels;
    }
}
