namespace Do.Domain.Model;

public record MethodModel(
    string Name,
    bool IsConstructor,
    OverloadModel[] Overloads
) : IModel
{
    string IModel.Id { get; } = Name;
}
