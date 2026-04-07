namespace Baked.Domain.Metadata;

public record PropertyMetadataModel(string Name, List<AttributeMetadataModel> Attributes);