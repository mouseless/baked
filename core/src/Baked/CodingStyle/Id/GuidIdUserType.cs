using Baked.DataAccess;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using System.Data.Common;

namespace Baked.CodingStyle.Id;

public class GuidIdUserType : UserTypeBase
{
    public override SqlType[] SqlTypes => [SqlTypeFactory.Guid];
    public override Type ReturnedType => typeof(Business.Id);

    public override object? NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object _)
    {
        var obj = (Guid?)NHibernateUtil.Guid.NullSafeGet(rs, names[0], session);

        return obj == null ? null : Baked.Business.Id.Parse(obj);
    }

    public override void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
    {
        if (value == null)
        {
            NHibernateUtil.Guid.NullSafeSet(cmd, null, index, session);
            return;
        }

        if (!Guid.TryParse(value.ToString(), out var guid))
        {
            throw new FormatException($"'{value}' was not recognized as a valid Guid format");
        }

        NHibernateUtil.Guid.NullSafeSet(cmd, guid, index, session);
    }
}