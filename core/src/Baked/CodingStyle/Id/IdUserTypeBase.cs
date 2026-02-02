using Baked.DataAccess;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using System.Data.Common;

namespace Baked.CodingStyle.Id;

public abstract class IdUserTypeBase<T> : UserTypeBase
{
    public abstract override SqlType[] SqlTypes { get; }
    public abstract T Parse(string value);

    public override Type ReturnedType => typeof(Business.Id);

    public override object? NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object _)
    {
        var obj = (T?)NHibernateUtil.Guid.NullSafeGet(rs, names[0], session);

        return obj == null ? null : Baked.Business.Id.Parse(obj);
    }

    public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
    {
        if (value == null)
        {
            NHibernateUtil.Guid.NullSafeSet(cmd, null, index, session);

            return;
        }

        NHibernateUtil.Guid.NullSafeSet(cmd, Parse($"{value}"), index, session);
    }
}