using System.Collections.ObjectModel;

namespace Do.Domain.Model;

public record MethodModel(
    string Name,
    ReadOnlyCollection<MethodOverloadModel> Overloads,
    AttributeCollection CustomAttributes
) : IModel, ICustomAttributesModel
{
    string IModel.Id => Name;
}
