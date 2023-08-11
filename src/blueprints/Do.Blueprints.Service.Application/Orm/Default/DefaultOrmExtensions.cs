using Do.Orm;
using Do.Orm.Default;

namespace Do;

public static class DefaultOrmExtensions
{
    public static DefaultOrmFeature Default(this OrmConfigurator source) => new();
}
