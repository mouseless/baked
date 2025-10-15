namespace Baked.Runtime;

public class ServiceProviderAccessor(IServiceProvider _root, IEnumerable<IServiceProviderAccessor> _accessors)
    : IServiceProviderAccessor
{
    public IServiceProvider GetServiceProvider()
    {
        foreach (var accessor in _accessors)
        {
            var result = accessor.GetServiceProvider();

            if (result is not null) { return result; }
        }

        return _root;
    }
}