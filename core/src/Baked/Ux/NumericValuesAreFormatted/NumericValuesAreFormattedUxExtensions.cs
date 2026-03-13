using Baked.Ux;
using Baked.Ux.NumericValuesAreFormatted;

namespace Baked;

public static class NumericValuesAreFormattedUxExtensions
{
    extension(UxConfigurator _)
    {
        public NumericValuesAreFormattedUxFeature NumericValuesAreFormatted() =>
            new();
    }
}