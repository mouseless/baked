using NHibernate.SqlTypes;
using NHibernate.Type;

namespace Do.Orm.Default.UserTypes;

public class JsonObjectStringType : AbstractStringType
{
    const int ColumnLength = 64 * 1024;

    public JsonObjectStringType()
        : base(new SqlType(System.Data.DbType.String, ColumnLength)) { }

    public override string Name => nameof(JsonObjectStringType);
}
