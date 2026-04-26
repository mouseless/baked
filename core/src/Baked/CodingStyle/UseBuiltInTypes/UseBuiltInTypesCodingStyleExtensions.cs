using Baked.CodingStyle;
using Baked.CodingStyle.UseBuiltInTypes;
using Baked.Domain.Model;
using System.Diagnostics.CodeAnalysis;

namespace Baked;

public static class UseBuiltInTypesCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public UseBuiltInTypesCodingStyleFeature UseBuiltInTypes(
            IEnumerable<string>? textPropertySuffixes = default
        ) => new(textPropertySuffixes ?? ["Data", "Description"]);
    }

    extension(TypeModel type)
    {
        public bool TryGetElementType([NotNullWhen(true)] out TypeModel? elementType)
        {
            elementType = default;

            if (!type.IsAssignableTo(typeof(IEnumerable<>))) { return false; }
            if (!type.TryGetGenerics(out var enumerableGenerics)) { return false; }

            elementType = type.IsArray
                ? enumerableGenerics.ElementType
                : enumerableGenerics.GenericTypeArguments.FirstOrDefault()?.Model;

            return elementType is not null;
        }

        public TypeModel GetElementType()
        {
            if (!type.TryGetElementType(out var result))
            {
                throw DiagnosticCode.RequiresElementType.Exception(
                    $"{type.Name} does not have an element type"
                );
            }

            return result;
        }
    }
}