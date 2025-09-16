using Baked.Ux;
using Baked.Ux.NumericValuesAreFormatted;

namespace Baked;

public static class NumericValuesAreFormattedUxExtensions
{
    public static NumericValuesAreFormattedUxFeature NumericValuesAreFormatted(this UxConfigurator _) =>
        new();
}