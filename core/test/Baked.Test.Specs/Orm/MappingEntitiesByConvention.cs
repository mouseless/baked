using Baked.Test.Lifetime;

using NHConfiguration = NHibernate.Cfg.Configuration;

namespace Baked.Test.Orm;

public class MappingEntitiesByConvention : TestSpec
{
    [Test]
    public void Maps_only_entities()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        configuration.GetClassMapping(typeof(Entity)).ShouldNotBeNull();
        configuration.GetClassMapping(typeof(Singleton)).ShouldBeNull();
    }

    [Test]
    public void Table_name_is_entity_class_name()
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
    public void Id_is__property_named_Id()
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