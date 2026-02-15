using Baked.DataAccess;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.Type;
using System.Data.Common;

namespace Baked.CodingStyle.Id;

public abstract class IdUserTypeBase : UserTypeBase
{
    public abstract override SqlType[] SqlTypes { get; }
    public abstract NullableType NHibernateType { get; }
    public abstract object Convert(object value);

    public override Type ReturnedType => typeof(Business.Id);

    public override object? NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object _)
    {
        var obj = NHibernateType.NullSafeGet(rs, names, session);

        return obj == null ? null : Business.Id.Create(obj);
    }

    public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
    {
        if (value == null)
        {
            NHibernateType.NullSafeSet(cmd, null, index, session);

            return;
        }

        NHibernateType.NullSafeSet(cmd, Convert(value), index, session);
    }
}