using Baked.Playground.Lifetime;
using Baked.Playground.Orm;

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
    public void Lazy_loading_is_enabled()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        configuration.GetClassMapping(typeof(Entity)).IsLazy.ShouldBeTrue();
    }
}