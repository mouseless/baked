using NHibernate.Dialect;
using System.Data;

namespace Do.Database.Dialects;

public class SQLliteWithObjectDialect : SQLiteDialect
{
    public SQLliteWithObjectDialect()
    {
        RegisterColumnType(DbType.Object, "MEDIUMTEXT");
    }
}
