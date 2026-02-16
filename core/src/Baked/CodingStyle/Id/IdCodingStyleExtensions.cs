using Baked.Business;
using Baked.CodingStyle;
using Baked.CodingStyle.Id;

namespace Baked;

public static class IdCodingStyleExtensions
{
    public static IdCodingStyleFeature Id(this CodingStyleConfigurator _) =>
        new();

    public static void Generated(this IdAttribute id) =>
        id.Mapping = new(typeof(IdGuidUserType)) { IdentifierGenerator = typeof(IdGuidGenerator) };

    public static void AutoIncrement(this IdAttribute id) =>
        id.Mapping = new(typeof(IdIntUserType)) { IdentifierGenerator = typeof(NHibernate.Id.IdentityGenerator) };

    public static void Assigned(this IdAttribute id) =>
        id.Mapping = new(typeof(IdStringUserType)) { IdentifierGenerator = typeof(NHibernate.Id.Assigned) };
}