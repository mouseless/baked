using Baked.CodingStyle.PrimitiveViaParsable;
using Baked.Playground.CodingStyle.PrimitiveViaParsable;
using Baked.Test;
using NHibernate.Type;

using NHConfiguration = NHibernate.Cfg.Configuration;

namespace Baked.Test.CodingStyle;

public class MappingPrimitives : TestSpec
{
    [TestCase(nameof(EntityWithPrimitive.Value))]
    [TestCase(nameof(EntityWithPrimitive.ValueNullable))]
    public void Primitives_use_primitive_user_type_by_convention(string propertyName)
    {
        var configuration = GiveMe.The<NHConfiguration>();

        var mapping = configuration.GetClassMapping(typeof(EntityWithPrimitive));
        var property = configuration.GetClassMapping(typeof(EntityWithPrimitive)).PropertyIterator.FirstOrDefault(p => p.Name == propertyName);

        property
            .ShouldNotBeNull()
            .Type.ShouldBeOfType<CustomType>()
            .UserType.ShouldBeOfType<PrimitiveUserType<Value>>();
    }

    [Test]
    public void Allows_insert_and_query_like_a_regular_string()
    {
        var expected = GiveMe.An<EntityWithPrimitive>().With(Value.Parse("test"));
        var entities = GiveMe.The<EntityWithPrimitives>();

        var actual = entities.By(value: Value.Parse("test")).FirstOrDefault();

        actual.ShouldNotBeNull();
        actual.ShouldBe(expected);
        actual.Value.ShouldBe("test");
        actual.ValueNullable.ShouldBe("test");
        actual.ValueNullableNull.ShouldBeNull();
    }
}