using Baked.Playground.Orm;

using NHConfiguration = NHibernate.Cfg.Configuration;

namespace Baked.Test.CodingStyle;

public class ConfiguringGuidIdAsIdentity : TestSpec
{
    [Test]
    public void Id_is_always_guid()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        configuration.GetClassMapping(typeof(Entity)).Identifier.Type.Name.ShouldBe("Guid");
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