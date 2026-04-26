using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class MethodModelContext : TypeModelMembersContext
{
    public required MethodModel Method { get; init; }

    public override string Identifier => $"{Type.CSharpFriendlyFullName}.{Method.Name}";
}