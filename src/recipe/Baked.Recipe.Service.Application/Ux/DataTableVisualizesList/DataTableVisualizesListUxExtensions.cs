using Baked.Ux;
using Baked.Ux.DataTableVisualizesList;

namespace Baked;

public static class DataTableVisualizesListUxExtensions
{
    public static DataTableVisualizesListUxFeature DataTableVisualizesList(this UxConfigurator _) =>
        new();
}