namespace Baked.Domain.Metadata;

public record MethodMetadataModel(string Name, List<AttributeMetadataModel> Attributes)
{
    public List<ParameterMetadataModel> Parameters { get; init; } = [];
}