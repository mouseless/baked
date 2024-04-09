using NHibernate.SqlTypes;
using NHibernate.Type;

namespace Do.CodingStyle.ObjectAsJson;

public class JsonObjectStringType()
    : AbstractStringType(new SqlType(System.Data.DbType.String, ColumnLength))
{
    const int ColumnLength = 64 * 1024;

    public override string Name => nameof(JsonObjectStringType);
}