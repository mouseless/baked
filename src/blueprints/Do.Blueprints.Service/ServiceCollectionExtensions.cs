using Do;
using Do.Blueprints.Service;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDo(this IServiceCollection source, Action<Builder> build)
    {
        Banner.Print();

        build(new Builder());

        return source;
    }
}
