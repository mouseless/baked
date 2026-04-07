namespace Baked.Domain.Metadata;

public record MethodMetadataModel(string Name, List<AttributeMetadataModel> Attributes,
     List<ParameterMetadataModel>? Parameters = default
);