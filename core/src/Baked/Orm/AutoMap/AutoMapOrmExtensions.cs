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

    extension(AttributeExportCollection collection)
    {
        public void AutoMap(Action<AttributeExport> configure) =>
            collection.Build("AutoMap", configure);
    }
}