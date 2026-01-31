using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Baked.RestApi;

public class ExtendedContractResolver : CamelCasePropertyNamesContractResolver, IContractResolverWithServiceProvider
{
    readonly Dictionary<string, Action<JsonProperty, IServiceProvider>> _propertyConfigureMap = [];

    public Type? ProxyType { get; set; }

    IServiceProvider? _serviceProvider;
    IServiceProvider ServiceProvider => _serviceProvider ?? throw new InvalidOperationException("ServiceProvider is required but not set");
    IServiceProvider IContractResolverWithServiceProvider.ServiceProvider { set => _serviceProvider = value; }

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

    public override JsonContract ResolveContract(Type type)
    {
        if (type.IsAssignableTo(ProxyType))
        {
            type = type.BaseType ?? throw new($"Proxy type `{type.FullName}` should have a base type!!");
        }

        return base.ResolveContract(type);
    }

    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        var properties = base.CreateProperties(type, memberSerialization);
        foreach (var property in properties)
        {
            if (_propertyConfigureMap.TryGetValue(GetPropertyKey(type, property.PropertyName), out var configure))
            {
                configure(property, ServiceProvider);
            }
        }

        return properties;
    }

    protected override List<MemberInfo> GetSerializableMembers(Type objectType) =>
        base.GetSerializableMembers(objectType)
            .Where(m => m.IsOriginallyPublic() && (m is not PropertyInfo p || p.GetMethod?.IsOriginallyPublic() == true))
            .ToList();
}