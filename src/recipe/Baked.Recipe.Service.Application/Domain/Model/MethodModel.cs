using System.Collections.ObjectModel;
using System.Xml;

namespace Baked.Domain.Model;

public record MethodModel(
    string Name,
    MethodOverloadModel DefaultOverload,
    ReadOnlyCollection<MethodOverloadModel> Overloads,
    AttributeCollection CustomAttributes
) : IModel, ICustomAttributesModel, IDocumentedModel
{
    string IModel.Id => Name;

    public XmlNode? Documentation => DefaultOverload.Documentation;
}