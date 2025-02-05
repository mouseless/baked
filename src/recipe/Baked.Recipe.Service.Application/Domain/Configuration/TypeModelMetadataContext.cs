using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class TypeModelMetadataContext : DomainModelContext
{
    public required TypeModelMetadata Type { get; init; }
}