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

    public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session) =>
        cmd.Parameters[index].Value = value is DateOnly dateOnly
            ? dateOnly.ToDateTime(TimeOnly.MinValue)
            : DBNull.Value;
}