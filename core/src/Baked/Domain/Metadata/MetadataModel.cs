using Baked.Domain.Model;

namespace Baked.Domain.Metadata;

public record MetadataModel(
    ModelCollection<TypeMetadataModel> Types
);