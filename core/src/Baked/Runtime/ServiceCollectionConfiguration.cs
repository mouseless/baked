using Microsoft.Extensions.DependencyInjection;

namespace Baked.Runtime;

public class ServiceCollectionConfiguration(IServiceCollection _services)
{
    public IServiceCollection Services { get; } = _services;
}