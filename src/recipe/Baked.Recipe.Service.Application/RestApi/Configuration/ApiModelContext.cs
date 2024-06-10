using Baked.RestApi.Model;

namespace Baked.RestApi.Configuration;

public record ApiModelContext
{
    public required ApiModel Api { get; init; }
}