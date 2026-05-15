using Newtonsoft.Json.Serialization;
using NHibernate.Proxy;

namespace Baked.RestApi;

public class ProxyAwareValueProvider(IValueProvider? _valueProvider)
    : IValueProvider
{
    public object? GetValue(object target)
    {
        if (target is INHibernateProxy proxy && proxy.HibernateLazyInitializer.IsUninitialized)
        {
            return default;
        }

        return _valueProvider?.GetValue(target);
    }

    public void SetValue(object target, object? value)
    {
        _valueProvider?.SetValue(target, value);
    }
}