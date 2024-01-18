namespace Do.Domain.Model;

public record MethodModel(
    string Name,
    OverloadModel[] Overloads,
    bool IsConstructor = false
) : IModel
{
    string IModel.Id { get; } = Name;
}
