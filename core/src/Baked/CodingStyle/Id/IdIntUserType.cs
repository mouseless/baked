using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.Type;

namespace Baked.CodingStyle.Id;

public class IdIntUserType : IdUserTypeBase
{
    public override SqlType[] SqlTypes => [SqlTypeFactory.UInt32];
    public override NullableType NHibernateType => NHibernateUtil.UInt32;

    public override object Convert(object value) =>
        value is uint @uint ? @uint :
        value is int @int ? (uint)@int :
        uint.Parse($"{value}");
}