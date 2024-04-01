namespace Do.Domain.Model;

public record MethodGroupModel(
    string Name,
    List<MethodModel> Methods,
    AttributeCollection CustomAttributes
) : ICustomAttributesModel, IKeyedModel
{
    string IKeyedModel.Id => Name;
}
