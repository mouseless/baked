using Baked.CodingStyle;
using Baked.CodingStyle.ValueType;
using Baked.Domain.Model;

namespace Baked;

public static class ValueTypeCodingStyleExtensions
{
    public static ValueTypeCodingStyleFeature ValueType(this CodingStyleConfigurator _) =>
        new();

    public static bool ImplementsIParsable(this TypeModel type) =>
        type.Apply(t => t
            .GetInterfaces()
            .Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IParsable<>) &&
                i.GenericTypeArguments[0] == t
            )
        );
}