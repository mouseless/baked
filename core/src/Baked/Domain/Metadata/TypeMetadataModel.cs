using Baked.Domain.Model;

namespace Baked.Domain.Metadata;

public class TypeMetadataModel(string id, string name) : IModel
{
    public string Id { get; set; } = id;
    public string Name { get; set; } = name;
    public List<AttributeMetadataModel> Attributes { get; set; } = [];
    public List<MethodMetadataModel> Methods { get; set; } = [];
    public List<PropertyMetadataModel> Properties { get; set; } = [];

    public record AttributeMetadataModel(string Type, params (string, object)[]? Values);

    public record MethodMetadataModel(string Name)
    {
        public List<AttributeMetadataModel> Attributes { get; set; } = [];
    }

    public record PropertyMetadataModel(string Name)
    {
        public List<AttributeMetadataModel> Attributes { get; set; } = [];
    }
}