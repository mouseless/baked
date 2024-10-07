using Microsoft.Extensions.DependencyInjection;

namespace Baked.Runtime.DependencyInjection;

public interface IServiceAdder
{
    void AddServices(IServiceCollection services);
}