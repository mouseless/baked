using Baked.Domain.Model;

namespace Baked.Domain.Export;

public record TypeExportModel(string Id, string Name) : IModel
{
    public string GroupName { get; set; } = Name;
    public List<AttributeExportModel> Attributes { get; init; } = [];
    public List<MethodExportModel> Methods { get; init; } = [];
    public List<PropertyExportModel> Properties { get; init; } = [];
}