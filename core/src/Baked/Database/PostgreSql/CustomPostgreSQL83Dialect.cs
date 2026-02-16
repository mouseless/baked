using NHibernate;
using NHibernate.Dialect.Function;

namespace Baked.Database.PostgreSql;

public class CustomPostgreSQL83Dialect : NHibernate.Dialect.PostgreSQL83Dialect
{
    public CustomPostgreSQL83Dialect()
    {
        RegisterFunction("date", new SQLFunctionTemplate(NHibernateUtil.Date, "cast(cast(?1 as date) as timestamp)"));
    }
}