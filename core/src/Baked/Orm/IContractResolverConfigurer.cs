using Baked.RestApi;

namespace Baked.Orm;

public interface IContractResolverConfigurer
{
    Dictionary<Type, string> IdPropertyNames { get; }

    void Configure(ExtendedContractResolver resolver);
}