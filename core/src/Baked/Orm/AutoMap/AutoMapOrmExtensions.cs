using Baked.Orm;
using Baked.Orm.AutoMap;

namespace Baked;

public static class AutoMapOrmExtensions
{
    extension(OrmConfigurator _)
    {
        public AutoMapOrmFeature AutoMap() =>
            new();
    }
}