namespace Baked.Test.Test;

public class UsingNoneWhenNoComponentWasConfigured : TestSpec
{
    [Test]
    [Ignore("not implemented")]
    public void When_no_component_is_found__it_returns_None() => this.ShouldFail();

    [Test]
    [Ignore("not implemented")]
    public void When_used__None_leaves_a_warning_log_in_build_output() => this.ShouldFail();

    [Test]
    [Ignore("not implemented")]
    public void None_contains_component_path_to_build_a_sample_config_code() => this.ShouldFail();

    [Test]
    [Ignore("not implemented")]
    public void When_used_in_a_type__None_contains_type_info() => this.ShouldFail();

    [Test]
    [Ignore("not implemented")]
    public void When_used_in_a_property__None_contains_property_info() => this.ShouldFail();

    [Test]
    [Ignore("not implemented")]
    public void When_used_in_a_method__None_contains_method_info() => this.ShouldFail();

    [Test]
    [Ignore("not implemented")]
    public void When_used_in_a_parameter__None_contains_parameter_info() => this.ShouldFail();
}