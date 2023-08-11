using NHibernate.Dialect;
using System.Data;

namespace Do.Database.Dialects;

public class MySQL57WithObjectDialect : MySQL57Dialect
{
    public MySQL57WithObjectDialect()
    {
        RegisterColumnType(DbType.Object, "MEDIUMTEXT");
    }
}
