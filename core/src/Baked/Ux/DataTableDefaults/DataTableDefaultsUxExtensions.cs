using Baked.Ux;
using Baked.Ux.DataTableDefaults;

namespace Baked;

public static class DataTableDefaultsUxExtensions
{
    public static DataTableDefaultsUxFeature DataTableDefaults(this UxConfigurator _) =>
        new();
}