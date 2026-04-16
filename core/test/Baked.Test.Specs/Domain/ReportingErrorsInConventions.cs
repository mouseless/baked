namespace Baked.Test.Domain;

public class ReportingErrorsInConventions : TestSpec
{
    [Test]
    [Ignore("not implemented")]
    public void Domain_model_builder_continues_execution_even_if_an_exception_occurs_during_post_build() =>
        this.ShouldFail();

    [Test]
    [Ignore("not implemented")]
    public void It_continues_execution_for_the_same_convention_per_domain_member() =>
        this.ShouldFail();

    [Test]
    [Ignore("not implemented")]
    public void When_convention_throws_an_exception__domain_model_builder_adds_it_to_reported_errors() =>
        this.ShouldFail();

    [Test]
    [Ignore("not implemented")]
    public void After_post_build__domain_model_builder_delegates_reported_errors_to_given_error_handler() =>
        this.ShouldFail();
}