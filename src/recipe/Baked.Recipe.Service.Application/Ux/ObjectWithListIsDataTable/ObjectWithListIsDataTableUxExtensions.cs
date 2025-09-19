using Baked.Ux;
using Baked.Ux.ObjectWithListIsDataTable;

namespace Baked;

public static class ObjectWithListIsDataTableUxExtensions
{
    public static ObjectWithListIsDataTableUxFeature ObjectWithListIsDataTable(this UxConfigurator _) =>
        new();
}