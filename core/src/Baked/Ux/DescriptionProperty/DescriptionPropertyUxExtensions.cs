using Baked.Ux;
using Baked.Ux.DescriptionProperty;

namespace Baked;

public static class DescriptionPropertyUxExtensions
{
    public static DescriptionPropertyUxFeature DescriptionProperty(this UxConfigurator _) =>
        new();
}