using System.Reflection;

namespace Do.Domain.Model;

public record TypeModel(
    Type Type,
    string Name,
    List<MethodModel> Methods
)
{
    public TypeModel(Type type)
        : this(type, type.Name, [])
    {
        var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly) ?? [];

        foreach (var method in methodInfos)
        {
            Methods.Add(new(method));
        }
    }
}
