using Baked.Binding.Rest;
using Baked.Playground.Business;

namespace Baked.Test.Binding;

public class RevalidatingValidatableObjects : TestSpec
{
    [TestCase(nameof(MethodSamples.Validatable))]
    [TestCase(nameof(MethodSamples.ValidatableWithResults))]
    [TestCase(nameof(MethodSamples.AnnotatedRecordWithValidatable))]
    public void Actions_with_validatable_parameters_use_revalidation_filter(string methodName)
    {
        var method = GiveMe.TheTypeModel<MethodSamples>().GetMembers().Methods[methodName];

        method.GetAction().AdditionalAttributes.ShouldContain(attribute => attribute.Contains(nameof(RevalidateValidatableObjectsFilter)));
    }

    [TestCase(nameof(MethodSamples.RecordParameters))]
    [TestCase(nameof(MethodSamples.RecordListParameters))]
    [TestCase(nameof(MethodSamples.AnnotatedRecordParameters))]
    public void Actions_without_validatable_parameters_skip_revalidation_filter(string methodName)
    {
        var method = GiveMe.TheTypeModel<MethodSamples>().GetMembers().Methods[methodName];

        method.GetAction().AdditionalAttributes.ShouldNotContain(attribute => attribute.Contains(nameof(RevalidateValidatableObjectsFilter)));
    }
}