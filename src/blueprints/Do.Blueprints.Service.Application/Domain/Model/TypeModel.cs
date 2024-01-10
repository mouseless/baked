using System.Reflection;

namespace Do.Domain.Model;

public record TypeModel(
    Type Type,
    string Name,
    List<MethodModel> Methods,
    List<PropertyModel> Properties,
    bool IsAbstract,
    bool IsValueType
)
{
    public TypeModel(Type type)
        : this(type, type.Name, [], [], type.IsAbstract, type.IsValueType)
    {
        var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];

        foreach (var method in methodInfos)
        {
            Methods.Add(new(method));
        }

        var propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];

        foreach (var property in propertyInfos)
        {
            Properties.Add(new(property));
        }
    }
}
