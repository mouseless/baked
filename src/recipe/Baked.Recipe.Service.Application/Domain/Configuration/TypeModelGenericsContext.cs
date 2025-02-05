using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class TypeModelGenericsContext : DomainModelContext
{
    public required TypeModelGenerics Type { get; init; }
}