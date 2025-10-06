using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class TypeModelInheritanceContext : DomainModelContext
{
    public required TypeModelInheritance Type { get; init; }
}