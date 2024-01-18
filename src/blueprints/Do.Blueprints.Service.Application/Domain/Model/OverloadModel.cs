namespace Do.Domain.Model;

public record OverloadModel(
    bool IsPublic,
    bool IsProtected,
    bool IsVirtual,
    ModelCollection<ParameterModel> Parameters,
    ModelCollection<TypeModel> CustomAttributes,
    TypeModel? ReturnType = default
) : IModel
{
    public string Id { get; } = $"{ReturnType?.Id}[{string.Join(',', Parameters.Select(p => p.Id))}]";
}
