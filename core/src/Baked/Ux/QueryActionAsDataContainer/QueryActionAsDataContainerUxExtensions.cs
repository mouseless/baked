using Baked.Ux;
using Baked.Ux.QueryActionAsDataContainer;

namespace Baked;

public static class QueryActionAsDataContainerUxExtensions
{
    extension(UxConfigurator _)
    {
        public QueryActionAsDataContainerUxFeature QueryActionAsDataContainer(
            int[]? pageSizeOptions = default
        ) => new(pageSizeOptions ?? [10, 20, 50, 100]);
    }
}