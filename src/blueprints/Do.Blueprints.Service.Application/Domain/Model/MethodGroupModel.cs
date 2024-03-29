namespace Do.Domain.Model;

public record MethodGroupModel(
    string Name,
    List<MethodModel> Methods,
    AttributeCollection CustomAttributes
) : IMemberModel
{
    string IModel.Id => Name;
}
