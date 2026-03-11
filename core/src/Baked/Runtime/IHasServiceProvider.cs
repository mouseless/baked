namespace Baked.Runtime;

public interface IHasServiceProvider
{
    IServiceProvider ServiceProvider { get; }
}