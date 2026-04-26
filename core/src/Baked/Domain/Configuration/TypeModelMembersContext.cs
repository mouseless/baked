using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class TypeModelMembersContext : DomainModelContext
{
    public required TypeModelMembers Type { get; init; }

    public override string Identifier => Type.CSharpFriendlyFullName;
}