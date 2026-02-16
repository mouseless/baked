using Baked.CodingStyle;
using Baked.CodingStyle.ValueType;

namespace Baked;

public static class ValueTypeCodingStyleExtensions
{
    public static ValueTypeCodingStyleFeature ValueType(this CodingStyleConfigurator _) =>
        new();
}