using Baked.Domain.Model;

namespace Baked.Domain.Metadata;

public record MetadataSetModel(ModelCollection<TypeMetadataModel> Types);