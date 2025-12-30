using Baked.Ux;
using Baked.Ux.PropertiesAsFieldset;

namespace Baked;

public static class PropertiesAsFieldsetUxExtensions
{
    public static PropertiesAsFieldsetUxFeature PropertiesAsFieldset(this UxConfigurator _) =>
        new();
}