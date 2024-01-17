namespace Do.Domain.Model;

public record OverloadModel(
    bool IsPublic,
    bool IsProtected,
    bool IsVirtual,
    ModelCollection<ParameterModel> Parameters,
    ModelCollection<AttributeModel> CustomAttributes,
    TypeModel? ReturnType = default
) : IModel
{
    public string Id { get; } = $"[{string.Join(',', Parameters.Select(p => p.Id))}]";
}
