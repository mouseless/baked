using Microsoft.Extensions.DependencyInjection;

namespace Baked.Runtime;

public class ServiceCollectionWrapper(IServiceCollection _services)
{
    public IServiceCollection Services { get; } = _services;
}