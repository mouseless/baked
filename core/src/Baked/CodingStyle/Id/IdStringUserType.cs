using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.Type;

namespace Baked.CodingStyle.Id;

public class IdStringUserType : IdUserTypeBase
{
    public override SqlType[] SqlTypes => [SqlTypeFactory.GetAnsiString(1024)];
    public override NullableType NHibernateType => NHibernateUtil.AnsiString;
    public override object Convert(object value) =>
        value is string @string ? @string : $"{value}";
}