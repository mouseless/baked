using Baked.Playground.Orm;
using Baked.RestApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Baked.Playground.Override.RestApi;

public class CustomContractResolver : DefaultContractResolver, IServiceProvideredContractResolver
{
    public IServiceProvider? ServiceProvider { get; set; }

    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        var properties = base.CreateProperties(type, memberSerialization);

        foreach (var property in properties)
        {
            if (property.PropertyType == typeof(Parent))
            {
                property.Converter = new ParentJsonConverter(ServiceProvider!);
            }
        }

        return properties;
    }
}