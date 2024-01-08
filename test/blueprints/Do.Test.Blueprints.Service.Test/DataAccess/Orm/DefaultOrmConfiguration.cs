using NHConfiguration = NHibernate.Cfg.Configuration;

namespace Do.Test.DataAccess.Orm;

public class DefaultOrmConfiguration : TestServiceSpec
{
    [Test]
    public void Maps_entities()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        configuration.GetClassMapping(typeof(Entity)).ShouldNotBeNull();
    }

    [Test]
    public void Table_name_is_entity_name()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        configuration.GetClassMapping(typeof(Entity)).Table.Name.ShouldBe(typeof(Entity).Name);
    }

    [Test]
    public void Id_is_always_guid()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        configuration.GetClassMapping(typeof(Entity)).Identifier.Type.Name.ShouldBe("Guid");
    }

    [Test]
    public void Id_is_property_name_Id()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        var actual = configuration.GetClassMapping(typeof(Entity)).Identifier.ColumnIterator.FirstOrDefault();
        actual.ShouldNotBeNull();
        actual.Text.ShouldBe("Id");
    }

    [Test]
    public void Lazy_loading_is_enabled()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        configuration.GetClassMapping(typeof(Entity)).IsLazy.ShouldBeTrue();
    }
}
