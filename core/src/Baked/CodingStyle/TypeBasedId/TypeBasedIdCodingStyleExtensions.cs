using Baked.Business;
using Baked.CodingStyle;
using Baked.CodingStyle.TypeBasedId;

namespace Baked;

public static class TypeBasedIdCodingStyleExtensions
{
    extension(CodingStyleConfigurator _)
    {
        public TypeBasedIdCodingStyleFeature TypeBasedId() =>
            new();
    }

    extension(IdProperty id)
    {
        public void Generated() =>
            id.Mapping = new(typeof(IdGuidUserType)) { IdentifierGenerator = typeof(IdGuidGenerator) };

        public void AutoIncrement() =>
            id.Mapping = new(typeof(IdIntUserType)) { IdentifierGenerator = typeof(NHibernate.Id.IdentityGenerator) };

        public void Assigned() =>
            id.Mapping = new(typeof(IdStringUserType)) { IdentifierGenerator = typeof(NHibernate.Id.Assigned) };

        public void AssignedGuid() =>
            id.Mapping = new(typeof(IdGuidUserType)) { IdentifierGenerator = typeof(NHibernate.Id.Assigned) };

        public IdProperty.MappingOptions GetMapping() =>
            id.Mapping ??
            new(typeof(IdGuidUserType)) { IdentifierGenerator = typeof(IdGuidGenerator) };
    }
}