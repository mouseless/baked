using Baked.Domain.Model;

namespace Baked.Domain.Metadata;

public record TypeMetadataModel(string Id, string Name) : IModel
{
    public string GroupName { get; set; } = Name;
    public List<AttributeMetadataModel> Attributes { get; init; } = [];
    public List<MethodMetadataModel> Methods { get; init; } = [];
    public List<PropertyMetadataModel> Properties { get; init; } = [];
}