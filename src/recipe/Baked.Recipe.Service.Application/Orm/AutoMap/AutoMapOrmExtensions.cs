using Do.Orm;
using Do.Orm.AutoMap;

namespace Do;

public static class AutoMapOrmExtensions
{
    public static AutoMapOrmFeature AutoMap(this OrmConfigurator _) =>
        new();
}