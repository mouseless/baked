using Baked.RestApi;
using Humanizer;
using Newtonsoft.Json.Serialization;

namespace Baked.CodingStyle.Locatable;

public class ProxyAwareValueProvider(ExtendedContractResolver _contractResolver, Dictionary<Type, string> _idPropertyNames, string? _propertyName, IValueProvider? _valueProvider)
    : IValueProvider
{
    object? IValueProvider.GetValue(object target)
    {
        if (!_contractResolver.IsUnitializedProxy(target))
        {
            return _valueProvider?.GetValue(target);
        }

        var type = _contractResolver.ClearProxyType(target.GetType());
        if (!_idPropertyNames.TryGetValue(type, out var idName)) { return default; }
        if (_propertyName != idName.Camelize()) { return default; }

        return _valueProvider?.GetValue(target);
    }

    void IValueProvider.SetValue(object target, object? value) =>
        _valueProvider?.SetValue(target, value);
}