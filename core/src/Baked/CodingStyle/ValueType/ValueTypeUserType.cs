using Baked.DataAccess;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using System.Data.Common;

namespace Baked.CodingStyle.ValueType;

public class ValueTypeUserType<T> : UserTypeBase
    where T : struct, IParsable<T>
{
    public override SqlType[] SqlTypes => [SqlTypeFactory.GetString(255)];
    public override Type ReturnedType => typeof(T);

    public override object? NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object _)
    {
        var obj = NHibernateUtil.String.NullSafeGet(rs, names, session);

        if (obj is null) { return null; }
        if (obj is not string s) { return null; }
        if (!T.TryParse(s, null, out var result)) { return null; }

        return result;
    }

    public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
    {
        if (value is null)
        {
            NHibernateUtil.String.NullSafeSet(cmd, null, index, session);

            return;
        }

        NHibernateUtil.String.NullSafeSet(cmd, value.ToString(), index, session);
    }
}