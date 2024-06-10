using Do.CodingStyle;
using Do.CodingStyle.UseBuiltInTypes;
using Do.Domain.Model;
using System.Diagnostics.CodeAnalysis;

namespace Do;

public static class UseBuiltInTypesCodingStyleExtensions
{
    public static UseBuiltInTypesCodingStyleFeature UseBuiltInTypes(this CodingStyleConfigurator _,
        IEnumerable<string>? textPropertySuffixes = default
    ) => new(textPropertySuffixes ?? ["Data"]);

    public static bool TryGetElementType(this TypeModel type, [NotNullWhen(true)] out TypeModel? elementType)
    {
        elementType = default;

        if (!type.IsAssignableTo(typeof(IEnumerable<>))) { return false; }
        if (!type.TryGetGenerics(out var enumerableGenerics)) { return false; }

        elementType = type.IsArray
            ? enumerableGenerics.ElementType
            : enumerableGenerics.GenericTypeArguments.FirstOrDefault()?.Model;

        return elementType is not null;
    }
}