using NHibernate.SqlTypes;

namespace Baked.CodingStyle.Id;

public class IdStringUserType : IdUserTypeBase<string>
{
    public override SqlType[] SqlTypes => [SqlTypeFactory.GetAnsiString(1024)];
    public override string Parse(string value) => value;
}