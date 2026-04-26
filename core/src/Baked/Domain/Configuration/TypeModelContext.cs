using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class TypeModelContext : DomainModelContext
{
    public required TypeModel Type { get; init; }

    public override string Identifier => Type.CSharpFriendlyFullName;
}