using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class ParameterModelContext : MethodModelContext
{
    public required MethodOverloadModel MethodOverload { get; init; }
    public required ParameterModel Parameter { get; init; }

    public override string Identifier => $"{Type.CSharpFriendlyFullName}.{Method.Name}.{Parameter.Name}";
}