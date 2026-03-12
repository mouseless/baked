using Baked.Ux;
using Baked.Ux.DescriptionProperty;

namespace Baked;

public static class DescriptionPropertyUxExtensions
{
    extension(UxConfigurator _)
    {
        public DescriptionPropertyUxFeature DescriptionProperty() =>
            new();
    }
}