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

    public object? NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object _)
    {
        var obj = (System.Guid?)NHibernateUtil.Guid.NullSafeGet(rs, names[0], session);

        return obj == null ? null : Orm.Id.Parse(obj);
    }

    public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
    {
        if (value == null)
        {
            NHibernateUtil.Guid.NullSafeSet(cmd, null, index, session);
            return;
        }

        // Ensure value type is always 'Orm.Id'
        if (value is not Orm.Id id)
        {
            throw new TypeMismatchException($"Expected type 'Orm.Id' but got {value.GetType().Name}");
        }

        // Ensure value is always guid
        if (!System.Guid.TryParse(id.ToString(), out var guid))
        {
            throw new FormatException($"'{id}' was not recognized as a valid Guid format");
        }

        NHibernateUtil.Guid.NullSafeSet(cmd, guid, index, session);
    }

    public bool IsMutable => false;
    public object DeepCopy(object value) => value;
    public new bool Equals(object x, object y) => object.Equals(x, y);
    public int GetHashCode(object x) => x?.GetHashCode() ?? 0;
    public object Replace(object original, object target, object owner) => original;
    public object Assemble(object cached, object owner) => cached;
    public object Disassemble(object value) => value;
}