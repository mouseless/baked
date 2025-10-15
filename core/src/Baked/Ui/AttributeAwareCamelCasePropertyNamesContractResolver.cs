using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Baked.Ui;

public class AttributeAwareCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
{
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization) =>
        type.IsAssignableTo(typeof(Attribute))
            ? [.. base.CreateProperties(type, memberSerialization).Where(p => p.PropertyName != "typeId")]
            : base.CreateProperties(type, memberSerialization);
}