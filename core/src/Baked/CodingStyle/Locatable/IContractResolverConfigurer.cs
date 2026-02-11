using Baked.RestApi;

namespace Baked.CodingStyle.Locatable;

public interface IContractResolverConfigurer
{
    Dictionary<Type, string> IdPropertyNames { get; }

    void Configure(ExtendedContractResolver resolver);
}