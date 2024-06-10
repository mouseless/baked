using ParameterModel = Baked.RestApi.Model.ParameterModel;

namespace Baked.RestApi.Configuration;

public record ParameterModelContext : ActionModelContext
{
    public required ParameterModel Parameter { get; init; }
}