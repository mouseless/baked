using Baked.Domain.Export;
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

    extension(AttributeExportConfigurations collection)
    {
        public void AutoMap(Action<AttributeExportConfiguration> configure) =>
            collection.Build("AutoMap", configure);
    }
}