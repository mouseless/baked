using Baked.CodingStyle.Id;
using Baked.Playground.CodingStyle;
using Baked.Playground.Orm;
using NHibernate.Mapping;
using NHConfiguration = NHibernate.Cfg.Configuration;

namespace Baked.Test.CodingStyle;

public class ConfiguringEntityIds : TestSpec
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
    public void Uses_values_from_id_attribute_to_configure_id_mapping()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        var classMapping = configuration.GetClassMapping(typeof(EntityWithIntId));

        classMapping.Identifier.Type.Name.ShouldBe(nameof(IdIntUserType));
        classMapping.Identifier.ShouldBeOfType<SimpleValue>().IdentifierGeneratorStrategy.ShouldContain(nameof(IdIntGenerator));
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
    public void Guid_id_maps_guid_with_auto_generate()
    {
        var newParent = GiveMe.The<Func<Parent>>();

        var parent = newParent().With(GiveMe.AString(), GiveMe.AString());

        Guid.TryParse(parent.Id.ToString(), out _).ShouldBeTrue();
    }

    [Test]
    public void Int_id_maps_to_uint_with_auto_increment()
    {
        var newEntityWithIntId = GiveMe.The<Func<EntityWithIntId>>();

        var result1 = newEntityWithIntId().With();
        var result2 = newEntityWithIntId().With();

        result1.Id.ToString().ShouldBe("1");
        result2.Id.ToString().ShouldBe("2");
    }

    [Test]
    public void Assigned_id_sets_id_to_given_value()
    {
        var newEntityWithAssignedId = GiveMe.The<Func<EntityWithAssignedId>>();

        var actual1 = newEntityWithAssignedId().With("1");
        var actual2 = newEntityWithAssignedId().With("string");

        actual1.Id.ToString().ShouldBe("1");
        actual2.Id.ToString().ShouldBe("string");
    }
}