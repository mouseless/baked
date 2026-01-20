using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using System.Data.Common;

namespace Baked.DataAccess;

public abstract class UserTypeBase : IUserType
{
    public abstract SqlType[] SqlTypes { get; }
    public abstract Type ReturnedType { get; }

    public abstract object? NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object _);
    public abstract void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session);

    public bool IsMutable => false;
    public object DeepCopy(object value) => value;
    public new bool Equals(object x, object y) => object.Equals(x, y);
    public int GetHashCode(object x) => x?.GetHashCode() ?? 0;
    public object Replace(object original, object target, object owner) => original;
    public object Assemble(object cached, object owner) => cached;
    public object Disassemble(object value) => value;
}