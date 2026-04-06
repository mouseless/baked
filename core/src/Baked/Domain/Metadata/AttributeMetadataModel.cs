namespace Baked.Domain.Metadata;

public record AttributeMetadataModel(string Type, params (string, object)[]? Values);