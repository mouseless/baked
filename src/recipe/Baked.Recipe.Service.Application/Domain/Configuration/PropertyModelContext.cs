using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class PropertyModelContext : TypeModelMembersContext
{
    public required PropertyModel Property { get; init; }
}