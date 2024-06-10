using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class PropertyModelContext : TypeModelMembersContext
{
    public required PropertyModel Property { get; init; }
}