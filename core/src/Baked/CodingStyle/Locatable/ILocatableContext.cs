using Baked.RestApi;

namespace Baked.CodingStyle.Locatable;

public interface ILocatableContext
{
    Dictionary<Type, string> IdPropertyNames { get; }

    void Configure(ExtendedContractResolver resolver);
}