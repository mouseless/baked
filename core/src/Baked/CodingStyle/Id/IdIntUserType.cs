using Baked.DataAccess;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using System.Data.Common;

namespace Baked.CodingStyle.Id;

public class IdIntUserType : UserTypeBase
{
    public override SqlType[] SqlTypes => [SqlTypeFactory.UInt32];
    public override Type ReturnedType => typeof(Business.Id);

    public override object? NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object _)
    {
        var obj = (uint?)NHibernateUtil.UInt32.NullSafeGet(rs, names[0], session);

        return obj == null ? null : Baked.Business.Id.Parse(obj);
    }

    public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
    {
        if (value is null or not uint)
        {
            NHibernateUtil.UInt32.NullSafeSet(cmd, null, index, session);

            return;
        }

        NHibernateUtil.UInt32.NullSafeSet(cmd, uint.Parse($"{value}"), index, session);
    }
}