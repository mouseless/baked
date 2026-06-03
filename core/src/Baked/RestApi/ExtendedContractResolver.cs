using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Baked.RestApi;

public class ExtendedContractResolver : CamelCasePropertyNamesContractResolver, IContractResolverWithServiceProvider
{
    readonly Dictionary<string, Action<JsonContract, IServiceProvider>> _typeConfigureMap = [];
    readonly Dictionary<string, Action<JsonProperty, IServiceProvider>> _propertyConfigureMap = [];
    Action<JsonProperty, IServiceProvider?> _valueProvider = (property, _) => { };
    Func<object, bool> _isUnitializedProxy = _ => false;
    Func<Type, Type> _clearProxyType = t => t;

    IServiceProvider? _serviceProvider;
    IServiceProvider ServiceProvider => _serviceProvider ?? throw new InvalidOperationException("ServiceProvider is required but not set");
    IServiceProvider IContractResolverWithServiceProvider.ServiceProvider { set => _serviceProvider = value; }

    string GetTypeKey(Type? type) =>
        $"{type?.AssemblyQualifiedName}";

    public void SetType(Type type, Action<JsonContract, IServiceProvider> options,
        bool @override = false
    )
    {
        var key = GetTypeKey(type);

        if (@override || !_typeConfigureMap.TryGetValue(key, out var old))
        {
            _typeConfigureMap[key] = options;

            return;
        }

        _typeConfigureMap[key] = (contract, sp) =>
        {
            old(contract, sp);
            options(contract, sp);
        };
    }

    string GetPropertyKey(Type? type, string? propertyName) =>
        $"{type?.AssemblyQualifiedName}-{propertyName?.ToLowerInvariant()}";

    public void SetProperty(Type type, string propertyName, Action<JsonProperty, IServiceProvider> options,
        bool @override = false
    )
    {
        var key = GetPropertyKey(type, propertyName);

        if (@override || !_propertyConfigureMap.TryGetValue(key, out var old))
        {
            _propertyConfigureMap[key] = options;

            return;
        }

        _propertyConfigureMap[key] = (property, sp) =>
        {
            old(property, sp);
            options(property, sp);
        };
    }

    public void SetValueProvider(Action<JsonProperty, IServiceProvider?> valueProvider) =>
        _valueProvider = valueProvider;

    public void SetIsUnitializedProxy(Func<object, bool> isUnitializedProxy) =>
        _isUnitializedProxy = isUnitializedProxy;

    public bool IsUnitializedProxy(object value) =>
        _isUnitializedProxy(value);

    public void SetClearProxyType(Func<Type, Type> clearProxyType) =>
        _clearProxyType = clearProxyType;

    public Type ClearProxyType(Type type) =>
        _clearProxyType(type);

    public override JsonContract ResolveContract(Type type)
    {
        type = ClearProxyType(type);

        var result = base.ResolveContract(type);
        if (_typeConfigureMap.TryGetValue(GetTypeKey(type), out var configure))
        {
            configure(result, ServiceProvider);
        }

        return result;
    }

    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        var result = base.CreateProperties(type, memberSerialization);
        foreach (var property in result)
        {
            if (_propertyConfigureMap.TryGetValue(GetPropertyKey(type, property.PropertyName), out var configure))
            {
                configure(property, ServiceProvider);
            }

            _valueProvider(property, _serviceProvider);
        }

        return result;
    }

    protected override List<MemberInfo> GetSerializableMembers(Type objectType) =>
    [
        .. base
            .GetSerializableMembers(objectType)
            .Where(m => m.IsOriginallyPublic())
    ];
}