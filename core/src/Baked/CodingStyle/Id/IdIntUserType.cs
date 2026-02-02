using NHibernate.SqlTypes;

namespace Baked.CodingStyle.Id;

public class IdIntUserType : IdUserTypeBase<uint>
{
    public override SqlType[] SqlTypes => [SqlTypeFactory.UInt32];
    public override uint Parse(string value) => uint.Parse(value);
}