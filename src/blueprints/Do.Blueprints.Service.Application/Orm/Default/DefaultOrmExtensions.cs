using Do.Orm;
using Do.Orm.Default;

namespace Do;

public static class DefaultOrmExtensions
{
    public static IOrmFeature Default(this OrmConfigurator _) => new DefaultOrmFeature();
}
