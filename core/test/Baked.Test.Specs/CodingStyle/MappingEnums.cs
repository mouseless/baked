using Baked.Playground.Orm;
using NHibernate.Type;

using NHConfiguration = NHibernate.Cfg.Configuration;

namespace Baked.Test.CodingStyle;

public class MappingEnums : TestSpec
{
    [Test]
    public void Normal_enums_map_by_string_value()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        var mapping = configuration.GetClassMapping(typeof(Entity));
        var property = configuration.GetClassMapping(typeof(Entity)).PropertyIterator.FirstOrDefault(p => p.Name is nameof(Entity.Enum));

        property
            .ShouldNotBeNull()
            .Type.ShouldBeOfType<EnumStringType<Enumeration>>();
    }

    [Test]
    public void Normal_query__allows_insert_and_query_like_a_regular_value()
    {
        var expected = GiveMe.An<Entity>().With(@enum: Enumeration.Member2);
        var entities = GiveMe.The<Entities>();
        GiveMe.TheSession(clear: true);

        var actual = entities.By(@enum: Enumeration.Member2).FirstOrDefault();

        actual.ShouldNotBeNull();
        actual.Id.ShouldBe(expected.Id);
        actual.Enum.ShouldBe(Enumeration.Member2);
    }

    [Test]
    public void Flags_enums_map_by_numeric_value()
    {
        var configuration = GiveMe.The<NHConfiguration>();

        var mapping = configuration.GetClassMapping(typeof(Entity));
        var property = configuration.GetClassMapping(typeof(Entity)).PropertyIterator.FirstOrDefault(p => p.Name is nameof(Entity.FlagsEnum));

        property
            .ShouldNotBeNull()
            .Type.ShouldBeOfType<EnumType<FlagsEnumeration>>();
    }

    [Test]
    public void Flags_query__allows_insert_and_query_like_a_regular_string()
    {
        var expected = GiveMe.An<Entity>().With(flagsEnum: FlagsEnumeration.Flag1 | FlagsEnumeration.Flag3);
        var entities = GiveMe.The<Entities>();
        GiveMe.TheSession(clear: true);

        var actual = entities.By(flagsEnum: FlagsEnumeration.Flag3).FirstOrDefault();

        actual.ShouldNotBeNull();
        actual.Id.ShouldBe(expected.Id);
        actual.FlagsEnum.ShouldBe(FlagsEnumeration.Flag1 | FlagsEnumeration.Flag3);
    }
}