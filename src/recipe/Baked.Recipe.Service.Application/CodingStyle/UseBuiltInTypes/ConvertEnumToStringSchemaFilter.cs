using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Runtime.Serialization;

namespace Baked.CodingStyle.UseBuiltInTypes;

public class ConvertEnumToStringSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum) { return; }

        schema.Type = "string";
        schema.Format = null;
        schema.Enum.Clear();

        foreach (var enumName in Enum.GetNames(context.Type))
        {
            var memberInfo = context.Type.GetMember(enumName).FirstOrDefault(m => m.DeclaringType == context.Type);
            var enumMemberAttribute = memberInfo?.GetCustomAttributes(typeof(EnumMemberAttribute), false).OfType<EnumMemberAttribute>().FirstOrDefault();

            var label = enumMemberAttribute == null || string.IsNullOrWhiteSpace(enumMemberAttribute.Value)
                ? enumName
                : enumMemberAttribute.Value;

            schema.Enum.Add(new OpenApiString(label.ToLowerInvariant()));
        }
    }
}