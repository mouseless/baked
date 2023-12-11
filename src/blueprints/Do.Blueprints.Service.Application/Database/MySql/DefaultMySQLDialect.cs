using NHibernate.Dialect;
using System.Data;

namespace Do.Database.MySql;

public class DefaultMySQLDialect : MySQL57Dialect
{
    public DefaultMySQLDialect()
    {
        RegisterColumnType(DbType.String, 65535, "VARCHAR($l)");
    }
}
