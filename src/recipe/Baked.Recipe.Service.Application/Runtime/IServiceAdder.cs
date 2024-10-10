using Microsoft.Extensions.DependencyInjection;

namespace Baked.Runtime;

public interface IServiceAdder
{
    void AddServices(IServiceCollection services);
}