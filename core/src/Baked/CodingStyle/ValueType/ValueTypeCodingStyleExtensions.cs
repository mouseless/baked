using Baked.CodingStyle;
using Baked.CodingStyle.ValueType;

namespace Baked;

public static class ValueTypeCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public ValueTypeCodingStyleFeature ValueType() =>
            new();
    }
}