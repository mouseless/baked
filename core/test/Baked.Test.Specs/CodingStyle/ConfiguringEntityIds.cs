using Baked.Business;
using Baked.CodingStyle.Id;
using Baked.Playground.CodingStyle.Id;
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

    [TestCase(typeof(EntityWithAutoIncrementId), nameof(IdIntUserType), nameof(NHibernate.Id.IdentityGenerator))]
    [TestCase(typeof(EntityWithAssignedId), nameof(IdStringUserType), nameof(NHibernate.Id.Assigned))]
    [TestCase(typeof(EntityWithAssignedGuidId), nameof(IdGuidUserType), nameof(NHibernate.Id.Assigned))]
    public void Uses_values_from_id_attribute_to_configure_id_mapping(Type entityType, string expectedTypeName, string expectedIdGeneratorStrategy)
    {
        var configuration = GiveMe.The<NHConfiguration>();

        var classMapping = configuration.GetClassMapping(entityType);

        classMapping.Identifier.Type.Name.ShouldBe(expectedTypeName);
        classMapping.Identifier.ShouldBeOfType<SimpleValue>().IdentifierGeneratorStrategy.ShouldContain(expectedIdGeneratorStrategy);
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
    public void Foreign_key_suffix_is_set_from_property_type_id_info()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        var actual = configuration.GetClassMapping(typeof(EntityWithAssignedId)).PropertyIterator.FirstOrDefault();
        actual.ShouldNotBeNull();
        actual.ColumnIterator.First().Text.ShouldBe("ForeignPrimaryKey");
    }

    [Test]
    public void Guid_id_maps_guid_with_generated()
    {
        var newParent = GiveMe.The<Func<Parent>>();

        var parent = newParent().With(GiveMe.AString(), GiveMe.AString());

        Guid.TryParse(parent.Id.ToString(), out _).ShouldBeTrue();
    }

    [Test]
    public void Int_id_maps_to_uint_with_auto_increment()
    {
        var newEntityWithIntId = GiveMe.The<Func<EntityWithAutoIncrementId>>();

        var result1 = newEntityWithIntId().With();
        var result2 = newEntityWithIntId().With();

        result1.PrimaryKey.ToString().ShouldBe("1");
        result2.PrimaryKey.ToString().ShouldBe("2");
    }

    [Test]
    public void Assigned_id_sets_id_to_given_value()
    {
        var newEntityWithAssignedId = GiveMe.The<Func<EntityWithAssignedId>>();

        var actual1 = newEntityWithAssignedId().With(Id.Parse("1"));
        var actual2 = newEntityWithAssignedId().With(Id.Parse("string"));

        actual1.Id.ToString().ShouldBe("1");
        actual2.Id.ToString().ShouldBe("string");
    }

    [Test]
    public void Assigned_guid_id_sets_id_to_given_value()
    {
        var newEntityWithAssignedId = GiveMe.The<Func<EntityWithAssignedGuidId>>();
        var id = Id.NewId();

        var actual = newEntityWithAssignedId().With(id);

        actual.Id.ShouldBe(id);
    }
}