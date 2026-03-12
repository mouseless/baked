using Baked.Business;
using Baked.CodingStyle;
using Baked.CodingStyle.Id;

namespace Baked;

public static class IdCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        // TODO - Should it be Property? ama benzerleri parametreli
        public IdCodingStyleFeature Id() =>
            new();
    }

    extension(IdAttribute id)
    {
        public void Generated() =>
            id.Mapping = new(typeof(IdGuidUserType)) { IdentifierGenerator = typeof(IdGuidGenerator) };

        public void AutoIncrement() =>
            id.Mapping = new(typeof(IdIntUserType)) { IdentifierGenerator = typeof(NHibernate.Id.IdentityGenerator) };

        public void Assigned() =>
            id.Mapping = new(typeof(IdStringUserType)) { IdentifierGenerator = typeof(NHibernate.Id.Assigned) };
    }
}