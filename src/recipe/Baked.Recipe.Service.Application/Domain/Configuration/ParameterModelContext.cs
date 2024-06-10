using Do.Domain.Model;

namespace Do.Domain.Configuration;

public class ParameterModelContext : MethodModelContext
{
    public required MethodOverloadModel MethodOverload { get; init; }
    public required ParameterModel Parameter { get; init; }
}