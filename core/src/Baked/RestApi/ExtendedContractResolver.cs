using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Baked.RestApi;

public class ExtendedContractResolver : CamelCasePropertyNamesContractResolver, IContractResolverWithServiceProvider
{
    public Type? ProxyType { get; set; }

    IServiceProvider? _serviceProvider;
    IServiceProvider ServiceProvider => _serviceProvider ?? throw new InvalidOperationException("ServiceProvider is required but not set");
    IServiceProvider IContractResolverWithServiceProvider.ServiceProvider { set => _serviceProvider = value; }

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
            if (property.PropertyType == typeof(Parent))
            {
                property.Converter = new ParentJsonConverter(ServiceProvider);
            }
        }

        return properties;
    }
}

public class LocatableJsonConverter<TLocatable>(IServiceProvider _sp)
    : JsonConverter<TLocatable>
{
    public override Parent ReadJson(JsonReader reader, Type objectType, Parent? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) { return null!; }
        if (reader.TokenType != JsonToken.StartObject) { throw new JsonSerializationException($"Expected object, got {reader.TokenType}"); }

        Id? id = default;
        while (reader.Read())
        {
            if (reader.TokenType == JsonToken.PropertyName)
            {
                var name = (string)reader.Value!;
                reader.Read();

                if (string.Equals(name, "id", StringComparison.OrdinalIgnoreCase))
                {
                    id = Id.Parse(reader.Value ?? "Whatt!!");
                }
                else
                {
                    reader.Skip();
                }
            }
            else if (reader.TokenType == JsonToken.EndObject)
            {
                break;
            }
        }

        if (!id.HasValue) { throw new("id is required"); }

        return _sp
            .UsingCurrentScope()
            .GetRequiredService<IQueryContext<Parent>>()
            .SingleById(id.Value);
    }

    public override TLocatable? ReadJson(JsonReader reader, Type objectType, TLocatable? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, TLocatable? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();

            return;
        }

        writer.WriteStartObject();
        writer.WritePropertyName(nameof(Parent.Id).Camelize());
        writer.WriteValue($"{value.Id}");
        writer.WritePropertyName(nameof(Parent.Name).Camelize());
        writer.WriteValue($"{value.Name}");
        writer.WriteEndObject();
    }
}