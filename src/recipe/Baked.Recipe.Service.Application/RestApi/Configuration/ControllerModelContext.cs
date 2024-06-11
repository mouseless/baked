using Baked.RestApi.Model;

namespace Baked.RestApi.Configuration;

public record ControllerModelContext : ApiModelContext
{
    public required ControllerModel Controller { get; init; }
}