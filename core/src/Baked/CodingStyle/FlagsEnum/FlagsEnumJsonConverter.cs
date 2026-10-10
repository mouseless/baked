using Humanizer;
using Newtonsoft.Json;
using System.Globalization;

namespace Baked.CodingStyle.FlagsEnum;

public class FlagsEnumJsonConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();

            return;
        }

        var intValue = Convert.ToInt32(value);
        var flags = Enum.GetValues(value.GetType())
            .Cast<object>()
            .Where(flag =>
            {
                var intFlag = Convert.ToInt32(flag);

                return intFlag != 0 && (intValue & intFlag) == intFlag;
            })
            .Select(flag => CultureInfo.UsingInvariantCulture(() => $"{flag}".Camelize()));

        serializer.Serialize(writer, flags);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var type = Nullable.GetUnderlyingType(objectType) ?? objectType;
        var names = serializer.Deserialize<string[]>(reader);
        if (names == null) { return null; }

        var result = 0;
        foreach (var name in names)
        {
            result |= (int)Enum.Parse(type, name, ignoreCase: true);
        }

        return Enum.ToObject(type, result);
    }

    public override bool CanConvert(Type objectType)
    {
        var type = Nullable.GetUnderlyingType(objectType) ?? objectType;

        return type.IsEnum && type.IsDefined(typeof(FlagsAttribute), false);
    }
}