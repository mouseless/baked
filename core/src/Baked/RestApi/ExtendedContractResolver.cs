using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Baked.RestApi;

public class ExtendedContractResolver : CamelCasePropertyNamesContractResolver, IContractResolverWithServiceProvider
{
    readonly Dictionary<string, Type> _propertyConverterMap = [];

    public Type? ProxyType { get; set; }

    IServiceProvider? _serviceProvider;
    IServiceProvider ServiceProvider => _serviceProvider ?? throw new InvalidOperationException("ServiceProvider is required but not set");
    IServiceProvider IContractResolverWithServiceProvider.ServiceProvider { set => _serviceProvider = value; }

    string GetConverterKey(Type? type, string? propertyName) =>
        $"{type?.AssemblyQualifiedName}-{propertyName?.ToLowerInvariant()}";

    public void SetPropertyConverterType(Type type, string propertyName, Type converterType)
    {
        _propertyConverterMap[GetConverterKey(type, propertyName)] = converterType;
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
            if (!_propertyConverterMap.TryGetValue(GetConverterKey(type, property.PropertyName), out var converterType)) { continue; }

            var converter = ServiceProvider.GetRequiredService(converterType);
            if (converter is not JsonConverter jsonConverter) { throw new InvalidOperationException($"`{converterType.Name}` is expected to be assignable to `JsonConverter`"); }

            property.Converter = jsonConverter;
        }

        return properties;
    }
}