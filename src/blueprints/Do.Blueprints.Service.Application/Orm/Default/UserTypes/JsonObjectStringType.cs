using NHibernate.SqlTypes;
using NHibernate.Type;

namespace Do.Orm.Default.UserTypes;

public class JsonObjectStringType : AbstractStringType
{
    public JsonObjectStringType()
        : base(new SqlType(System.Data.DbType.Object)) { }

    public override string Name => nameof(JsonObjectStringType);
}
