using Microsoft.Extensions.DependencyInjection;

namespace Baked.DependencyInjection;

public interface IServiceAdder
{
    void AddServices(IServiceCollection services);
}