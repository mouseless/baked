using Baked.RestApi.Model;

namespace Baked.RestApi.Configuration;

public record ActionModelContext : ControllerModelContext
{
    public required ActionModel Action { get; init; }
}