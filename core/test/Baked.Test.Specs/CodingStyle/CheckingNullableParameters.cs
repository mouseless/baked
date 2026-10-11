using Baked.Playground.CodingStyle.UseNullableTypes;

namespace Baked.Test.CodingStyle;

public class CheckingNullableParameters : TestSpec
{
    [TestCase(nameof(NullableSamples.ValueType))]
    [TestCase(nameof(NullableSamples.ReferenceType))]
    public void Only_non_nullable_parameters_should_have_not_null_attribute(string methodName)
    {
        var nullableSamples = GiveMe.TheTypeModel<NullableSamples>();
        var method = nullableSamples.GetMembers().Methods[methodName];
        var parameters = method.DefaultOverload.Parameters;

        parameters["notNull"].IsNullable.ShouldBeFalse();
        parameters["nullable"].IsNullable.ShouldBeTrue();
        parameters["optional"].IsNullable.ShouldBeFalse();
        parameters["optionalNullable"].IsNullable.ShouldBeTrue();
    }

    [TestCase(nameof(NullableSamples.ValueType))]
    [TestCase(nameof(NullableSamples.ReferenceType))]
    public void Optionals_or_nullables_are_not_required(string methodName)
    {
        var nullableSamples = GiveMe.TheTypeModel<NullableSamples>();
        var method = nullableSamples.GetMembers().Methods[methodName];
        var parameters = method.DefaultOverload.Parameters;

        parameters["notNull"].ShouldBeRequired();
        parameters["nullable"].ShouldNotBeRequired();
        parameters["optional"].ShouldNotBeRequired();
        parameters["optionalNullable"].ShouldNotBeRequired();
    }
}