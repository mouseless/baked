using Baked.CodingStyle;
using Baked.CodingStyle.ValueType;
using Baked.Domain.Model;

namespace Baked;

public static class ValueTypeCodingStyleExtensions
{
    public static ValueTypeCodingStyleFeature ValueType(this CodingStyleConfigurator _) =>
        new();

    public static bool IsCustomValueType(this TypeModel type) =>
        type.IsValueType &&
        !type.IsEnum &&
        type.Namespace is not null &&
        !type.Namespace.StartsWith("System") &&
        type.Apply(t => t.ImplementsIParsable());

    public static bool IsCustomValueType(this Type type) =>
        type.IsValueType &&
        !type.IsEnum &&
        type.Namespace is not null &&
        !type.Namespace.StartsWith("System") &&
        type.ImplementsIParsable();

    public static bool ImplementsIParsable(this Type type) =>
        type.GetInterfaces()
            .Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IParsable<>) &&
                i.GenericTypeArguments[0] == type
            );
}