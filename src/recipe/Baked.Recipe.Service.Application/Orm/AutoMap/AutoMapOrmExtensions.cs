using Baked.Orm;
using Baked.Orm.AutoMap;

namespace Baked;

public static class AutoMapOrmExtensions
{
    public static AutoMapOrmFeature AutoMap(this OrmConfigurator _) =>
        new();
}