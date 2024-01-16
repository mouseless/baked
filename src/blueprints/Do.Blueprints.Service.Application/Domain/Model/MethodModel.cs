namespace Do.Domain.Model;

public record MethodModel(
    string Name,
    TypeModel ReturnType,
    bool IsPublic,
    bool IsProtected,
    bool IsVirtual,
    ModelCollection<ParameterModel> Parameters,
    ModelCollection<TypeModel> CustomAttributes
) : IModel
{
    public string Id { get; } = $"{Name}[{string.Join(',', Parameters.Select(p => p.Id))}]";
}


