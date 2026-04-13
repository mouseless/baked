using Baked.Domain.Model;

namespace Baked.Domain.Export;

public record TypeAttributeExportModel(string Id, string Name) : IModel
{
    public string GroupName { get; set; } = Name;
    public List<AttributeExportModel> Attributes { get; init; } = [];
    public List<MethodAttributeExportModel> Methods { get; init; } = [];
    public List<PropertyAttributeExportModel> Properties { get; init; } = [];
}