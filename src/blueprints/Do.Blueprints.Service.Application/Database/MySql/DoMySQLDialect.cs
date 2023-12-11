using NHibernate.Dialect;
using System.Data;

namespace Do.Database.MySql;

public class DoMySQLDialect : MySQL57Dialect
{
    public DoMySQLDialect()
    {
        RegisterColumnType(DbType.String, 65535, "VARCHAR($l)");
    }
}
