using Baked.Ux;
using Baked.Ux.ListIsDataTable;

namespace Baked;

public static class ListIsDataTableUxExtensions
{
    extension(UxConfigurator _)
    {
        public ListIsDataTableUxFeature ListIsDataTable() =>
            new();
    }
}