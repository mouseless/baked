using NHibernate.Dialect;
using System.Data;

namespace Do.Database.MySql;

public class CustomDialect : MySQL57Dialect
{
    public CustomDialect()
    {
        RegisterColumnType(DbType.Object, "MEDIUMTEXT");
    }
}
