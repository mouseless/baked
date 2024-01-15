namespace Do.Domain.Model;

public record MethodModel(
    string Name,
    TypeModel ReturnType,
    bool IsPublic,
    ModelCollection<ParameterModel> Parameters,
    ModelCollection<TypeModel> CustomAttributes
) : IModel
{
    public string Id => $"{Name}[{string.Join(',', Parameters.Select(p => p.Id))}]";
}


