using NHibernate.SqlTypes;
using NHibernate.Type;

namespace Do.Orm.Default.UserTypes;

public class SerializedObjectType : AbstractStringType
{
    public SerializedObjectType()
        : base(new SqlType(System.Data.DbType.Object)) { }

    public override string Name => "SerializedObjectType";
}
