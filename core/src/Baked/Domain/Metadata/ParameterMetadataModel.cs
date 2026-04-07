namespace Baked.Domain.Metadata;

public record ParameterMetadataModel(string Name, List<AttributeMetadataModel> Attributes);