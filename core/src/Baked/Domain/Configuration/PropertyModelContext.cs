using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class PropertyModelContext : TypeModelMembersContext
{
    public required PropertyModel Property { get; init; }

    public override string Identifier => $"{Type.CSharpFriendlyFullName}.{Property.Name}";
}