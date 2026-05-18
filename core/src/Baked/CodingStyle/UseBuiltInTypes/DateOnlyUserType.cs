using Baked.DataAccess;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using System.Data.Common;

namespace Baked.CodingStyle.UseBuiltInTypes;

public class DateOnlyUserType : UserTypeBase
{
    public override SqlType[] SqlTypes => [NHibernateUtil.Date.SqlType];
    public override Type ReturnedType => typeof(DateOnly);

    public override object? NullSafeGet(DbDataReader dataReader, string[] names, ISessionImplementor session, object _)
    {
        var ordinal = dataReader.GetOrdinal(names[0]);
        if (dataReader.IsDBNull(ordinal)) { return null; }

        var raw = dataReader.GetValue(ordinal);
        if (raw is DateOnly date) { return date; }
        if (raw is DateTime datetime) { return DateOnly.FromDateTime(datetime); }

        return null;
    }

    public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
    {
        if (value is DateOnly dateOnly)
        {
            NHibernateUtil.Date.NullSafeSet(cmd, dateOnly.ToDateTime(TimeOnly.MinValue), index, session);

            return;
        }

        NHibernateUtil.Date.NullSafeSet(cmd, null, index, session);
    }
}
