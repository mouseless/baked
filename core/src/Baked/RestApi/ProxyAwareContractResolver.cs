using Newtonsoft.Json.Serialization;

namespace Baked.RestApi;

public class ProxyAwareContractResolver<TProxyInterface>(IContractResolver _real)
    : IContractResolver
{
    public JsonContract ResolveContract(Type type)
    {
        if (type.IsAssignableTo(typeof(TProxyInterface)))
        {
            type = type.BaseType ?? throw new($"Proxy type {type.FullName} should have a base type!!");
        }

        return _real.ResolveContract(type);
    }
}