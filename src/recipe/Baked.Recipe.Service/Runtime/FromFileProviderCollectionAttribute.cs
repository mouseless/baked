using Microsoft.Extensions.DependencyInjection;

namespace Baked.Runtime;

public class FromFileProviderCollectionAttribute : FromKeyedServicesAttribute
{
    public const string FILE_PROVIDERS_KEY = "FILE_PROVIDERS_KEY";

    public FromFileProviderCollectionAttribute()
        : base(FILE_PROVIDERS_KEY)
    { }
}