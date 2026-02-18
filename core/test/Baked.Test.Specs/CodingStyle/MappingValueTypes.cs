using Baked.CodingStyle.ValueType;
using Baked.Playground.CodingStyle.ValueType;
using Baked.Test;
using NHibernate.Type;

using NHConfiguration = NHibernate.Cfg.Configuration;

namespace Baked.Playground.CodingStyle;

public class MappingValueTypes : TestSpec
{
    [TestCase(nameof(EntityWithValueType.Value))]
    [TestCase(nameof(EntityWithValueType.ValueNullable))]
    public void Value_types_use_value_type_user_type_by_convention(string propertyName)
    {
        var configuration = GiveMe.The<NHConfiguration>();

        var mapping = configuration.GetClassMapping(typeof(EntityWithValueType));
        var property = configuration.GetClassMapping(typeof(EntityWithValueType)).PropertyIterator.FirstOrDefault(p => p.Name == propertyName);

        property?
            .Type.ShouldBeOfType<CustomType>()
            .UserType.ShouldBeOfType<ValueTypeUserType<Value>>();
    }

    [Test]
    public void Allows_insert_and_query_like_a_regular_string()
    {
        var expected = GiveMe.An<EntityWithValueType>().With(Value.Parse("test"));
        var entities = GiveMe.The<EntityWithValueTypes>();

        var actual = entities.By(value: Value.Parse("test")).FirstOrDefault();

        actual.ShouldNotBeNull();
        actual.ShouldBe(expected);
        actual.Value.ShouldBe(Value.Parse("test"));
        actual.ValueNullable.ShouldBe(Value.Parse("test"));
        actual.ValueNullableNull.ShouldBeNull();
    }
}