using Baked.Ux;
using Baked.Ux.NumericValuesAreFormatted;

namespace Baked;

public static class NumericValuesAreFormattedUxExtensions
{
    extension(UxConfigurator _)
    {
        // TODO - bunlarda da property e geçmek gerekir mi?
        public NumericValuesAreFormattedUxFeature NumericValuesAreFormatted() =>
            new();
    }
}