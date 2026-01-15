using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using System.Data.Common;

namespace Baked.Id.Guid;

public class GuidIdUserType : IUserType
{
    // TODO check if `SqlTypeFactory.Guid` works for all
    // database types
    public SqlType[] SqlTypes => [SqlTypeFactory.Guid];
    public Type ReturnedType => typeof(Orm.Id);

    public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object _)
    {
        var obj = NHibernateUtil.Guid.NullSafeGet(rs, names[0], session);

        return obj == null ?
            Orm.Id.Parse(System.Guid.Empty) :
            Orm.Id.Parse(System.Guid.Parse($"{obj}"));
    }

    public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
    {
        if (value == null)
        {
            NHibernateUtil.Guid.NullSafeSet(cmd, null, index, session);
        }
        else
        {
            var id = Orm.Id.Parse(System.Guid.Parse($"{value}"));

            NHibernateUtil.Guid.NullSafeSet(cmd, id.Value, index, session);
        }
    }

    public bool IsMutable => false;
    public object DeepCopy(object value) => value;
    public new bool Equals(object x, object y) => object.Equals(x, y);
    public int GetHashCode(object x) => x?.GetHashCode() ?? 0;
    public object Replace(object original, object target, object owner) => original;
    public object Assemble(object cached, object owner) => cached;
    public object Disassemble(object value) => value;
}