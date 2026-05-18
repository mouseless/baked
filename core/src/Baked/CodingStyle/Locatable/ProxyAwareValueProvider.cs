using Humanizer;
using Newtonsoft.Json.Serialization;
using NHibernate.Proxy;

namespace Baked.CodingStyle.Locatable;

public class ProxyAwareValueProvider(Dictionary<Type, string> _idPropertyNames, string? _propertyName, IValueProvider? _valueProvider)
    : IValueProvider
{
    object? IValueProvider.GetValue(object target)
    {
        if (target is not INHibernateProxy proxy || !proxy.HibernateLazyInitializer.IsUninitialized)
        {
            return _valueProvider?.GetValue(target);
        }

        if (!_idPropertyNames.TryGetValue(proxy.HibernateLazyInitializer.PersistentClass, out var idName)) { return default; }
        if (_propertyName != idName.Camelize()) { return default; }

        return _valueProvider?.GetValue(target);
    }

    void IValueProvider.SetValue(object target, object? value) =>
        _valueProvider?.SetValue(target, value);
}