namespace Do.Domain.Model;

public record OverloadModel(
    ModelCollection<ParameterModel> Parameters,
    ModelCollection<TypeModel> CustomAttributes
) : IModel
{
    public string Id { get; } = $"[{string.Join(',', Parameters.Select(p => p.Id))}]";
}
