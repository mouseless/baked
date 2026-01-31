using Baked.CodingStyle.Id;
using Baked.Playground.Orm;
using NHibernate.Mapping;

using NHConfiguration = NHibernate.Cfg.Configuration;

namespace Baked.Test.CodingStyle;

public class ConfiguringIdAsGuid : TestSpec
{
    [Test]
    public void Id_is_always_id_guid_user_type_with_id_guid_generator()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        var classMapping = configuration.GetClassMapping(typeof(Entity));

        classMapping.Identifier.Type.Name.ShouldBe(nameof(IdGuidUserType));
        classMapping.Identifier.ShouldBeOfType<SimpleValue>().IdentifierGeneratorStrategy.ShouldContain(nameof(IdGuidGenerator));
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