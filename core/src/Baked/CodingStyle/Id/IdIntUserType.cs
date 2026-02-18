using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.Type;

namespace Baked.CodingStyle.Id;

public class IdIntUserType : IdUserTypeBase
{
    public override SqlType[] SqlTypes => [SqlTypeFactory.Int32];
    public override NullableType NHibernateType => NHibernateUtil.Int32;

    public override object Convert(object value) =>
        value is int @int ? @int :
        int.Parse($"{value}");
}