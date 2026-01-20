using Baked.CodingStyle.Id;
using Baked.Playground.Orm;
using NHibernate.Mapping;
using NHConfiguration = NHibernate.Cfg.Configuration;

namespace Baked.Test.CodingStyle;

public class ConfiguringIdAsGuid : TestSpec
{
    [Test]
    public void Id_is_always_guid_id_user_type()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        var classMapping = configuration.GetClassMapping(typeof(Entity));

        classMapping.Identifier.Type.Name.ShouldBe(nameof(IdGuidUserType));
        ((SimpleValue)classMapping.Identifier).IdentifierGeneratorStrategy.ShouldContain(nameof(IdGuidGenerator));
    }

    [Test]
    public void Id_is__property_named_Id()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        var actual = configuration.GetClassMapping(typeof(Entity)).Identifier.ColumnIterator.FirstOrDefault();
        actual.ShouldNotBeNull();
        actual.Text.ShouldBe("Id");
    }
}