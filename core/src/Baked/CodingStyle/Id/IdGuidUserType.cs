using NHibernate.SqlTypes;

namespace Baked.CodingStyle.Id;

public class IdGuidUserType : IdUserTypeBase<Guid>
{
    public override SqlType[] SqlTypes => [SqlTypeFactory.Guid];
    public override Guid Parse(string value) => Guid.Parse($"{value}");
}