using Baked.Ux;
using Baked.Ux.ListIsDataTable;

namespace Baked;

public static class ListIsDataTableUxExtensions
{
    public static ListIsDataTableUxFeature ListIsDataTable(this UxConfigurator _) =>
        new();
}