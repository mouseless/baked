using Baked.DataAccess;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using System.Data.Common;

namespace Baked.CodingStyle.UseBuiltInTypes;

public class TimeOnlyUserType : UserTypeBase
{
    public override SqlType[] SqlTypes => [NHibernateUtil.Time.SqlType];
    public override Type ReturnedType => typeof(TimeOnly);

    public override object? NullSafeGet(DbDataReader dataReader, string[] names, ISessionImplementor session, object _)
    {
        var ordinal = dataReader.GetOrdinal(names[0]);
        if (dataReader.IsDBNull(ordinal)) { return null; }

        var raw = dataReader.GetValue(ordinal);
        if (raw is TimeOnly timeOnly) { return timeOnly; }
        if (raw is TimeSpan timeSpan) { return TimeOnly.FromTimeSpan(timeSpan); }
        if (raw is DateTime datetime) { return TimeOnly.FromDateTime(datetime); }

        return null;
    }

    public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
    {
        if (value is TimeOnly timeOnly)
        {
            NHibernateUtil.Time.NullSafeSet(cmd, timeOnly.ToTimeSpan(), index, session);

            return;
        }

        NHibernateUtil.Time.NullSafeSet(cmd, null, index, session);
    }
}
