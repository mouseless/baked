using Do.Orm;
using Do.Orm.Default;

namespace Do;

public static class DefatultOrmExtensions
{
    public static DefaultOrmFeature Default(this OrmConfigurator _) => new();
}
