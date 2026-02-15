using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.Type;

namespace Baked.CodingStyle.Id;

public class IdGuidUserType : IdUserTypeBase
{
    public override SqlType[] SqlTypes => [SqlTypeFactory.Guid];
    public override NullableType NHibernateType => NHibernateUtil.Guid;

    public override object Convert(object value) =>
        Guid.Parse($"{value}");
}