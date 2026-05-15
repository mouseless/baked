using Humanizer;
using Newtonsoft.Json.Serialization;
using NHibernate.Proxy;

namespace Baked.CodingStyle.Locatable;

public class ProxyAwareValueProvider(Dictionary<Type, string> _idPropertyNames)
    : IValueProvider
{
    string? _propertyName = default!;
    IValueProvider? _valueProvider = default!;

    public ProxyAwareValueProvider With(string? propertyName, IValueProvider? valueProvider)
    {
        _propertyName = propertyName;
        _valueProvider = valueProvider;

        return this;
    }

    object? IValueProvider.GetValue(object target)
    {
        if (target is INHibernateProxy proxy && proxy.HibernateLazyInitializer.IsUninitialized)
        {
            if (!_idPropertyNames.TryGetValue(proxy.HibernateLazyInitializer.PersistentClass, out var idName)) { return default; }
            if (_propertyName != idName.Camelize()) { return default; }

            return _valueProvider?.GetValue(target);
        }

        return _valueProvider?.GetValue(target);
    }

    void IValueProvider.SetValue(object target, object? value) =>
        _valueProvider?.SetValue(target, value);
}