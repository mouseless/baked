using NHibernate.SqlTypes;
using NHibernate.Type;

namespace Do.Orm.Default.UserTypes;

public class JsonObjectStringType : AbstractStringType
{
    public JsonObjectStringType()
        : base(new SqlType(System.Data.DbType.String, 64 * 1024)) { }

    public override string Name => nameof(JsonObjectStringType);
}
