using Baked.Ux;
using Baked.Ux.PropertiesAsFieldset;

namespace Baked;

public static class PropertiesAsFieldsetUxExtensions
{
    extension(UxConfigurator _)
    {
        public PropertiesAsFieldsetUxFeature PropertiesAsFieldset() =>
            new();
    }
}