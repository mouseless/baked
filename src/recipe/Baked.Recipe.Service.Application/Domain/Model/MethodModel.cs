using System.Collections.ObjectModel;

namespace Baked.Domain.Model;

public record MethodModel(
    string Name,
    MethodOverloadModel DefaultOverload,
    ReadOnlyCollection<MethodOverloadModel> Overloads,
    AttributeCollection CustomAttributes
) : IModel, ICustomAttributesModel
{
    string IModel.Id => Name;
}