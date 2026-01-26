using Newtonsoft.Json.Serialization;

namespace Baked.RestApi;

public interface IServiceProvideredContractResolver : IContractResolver
{
    IServiceProvider? ServiceProvider { get; set; }
}