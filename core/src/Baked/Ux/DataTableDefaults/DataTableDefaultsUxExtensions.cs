using Baked.Ux;
using Baked.Ux.DataTableDefaults;

namespace Baked;

public static class DataTableDefaultsUxExtensions
{
    extension(UxConfigurator _)
    {
        public DataTableDefaultsUxFeature DataTableDefaults() =>
            new();
    }
}