using Do.Orm;
using Do.Orm.NHibernate;

namespace Do;

public static class NHibernateOrmExtensions
{
    public static NHibernateOrmFeature NHibernate(this OrmConfigurator source) => new();
}
