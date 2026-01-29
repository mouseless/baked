using Newtonsoft.Json.Serialization;

namespace Baked.RestApi;

public interface IContractResolverWithServiceProvider : IContractResolver
{
    IServiceProvider ServiceProvider { set; }
}