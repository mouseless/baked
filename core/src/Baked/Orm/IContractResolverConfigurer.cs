using Baked.RestApi;

namespace Baked.Orm;

public interface IContractResolverConfigurer
{
    void Configure(ExtendedContractResolver resolver);
}