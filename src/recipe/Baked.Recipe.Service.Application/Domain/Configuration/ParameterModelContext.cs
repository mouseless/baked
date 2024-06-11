using Baked.Domain.Model;

namespace Baked.Domain.Configuration;

public class ParameterModelContext : MethodModelContext
{
    public required MethodOverloadModel MethodOverload { get; init; }
    public required ParameterModel Parameter { get; init; }
}