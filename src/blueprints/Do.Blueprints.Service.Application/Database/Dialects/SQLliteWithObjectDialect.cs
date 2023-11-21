using NHibernate.Dialect;
using System.Data;

namespace Do.Database.Dialects;

public class SQLliteWithObjectDialect : SQLiteDialect
{
    public SQLliteWithObjectDialect()
    {
        RegisterColumnType(DbType.String, 65535, "TEXT");
        RegisterColumnType(DbType.String, 16777215, "MEDIUMTEXT");
    }
}
