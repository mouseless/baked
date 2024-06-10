using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class MethodModelContext : TypeModelMembersContext
{
    public required MethodModel Method { get; init; }
}