namespace Baked.Test.Domain;

public class ReportingErrorsInConventions : TestSpec
{
    [AttributeUsage(AttributeTargets.All)]
    public class CustomAttribute : Attribute;

    [Test]
    public void Domain_model_builder_continues_execution_even_if_an_exception_occurs_during_post_build()
    {
        var hit = false;
        var builder = GiveMe.ADomainModelBuilder(options: builder =>
        {
            builder.Conventions.AddTypeAttribute(
                when: _ => true,
                attribute: () => throw new("test")
            );
            builder.Conventions.AddTypeAttribute(
                when: _ => true,
                attribute: () =>
                {
                    hit = true;

                    return new CustomAttribute();
                }
            );
        });
        var model = builder.Build([typeof(string)]);

        builder.PostBuild(model);

        hit.ShouldBeTrue();
    }

    [Test]
    [Ignore("not implemented")]
    public void After_post_build__domain_model_builder_delegates_reported_error_to_configured_error_handler() =>
        this.ShouldFail();

    [Test]
    [Ignore("not implemented")]
    public void It_allows_multiple_errors_in_one_execution() =>
        this.ShouldFail();

    [Test]
    [Ignore("not implemented")]
    public void It_continues_execution_for_the_same_convention_per_domain_member() =>
        this.ShouldFail();

    [Test]
    [Ignore("not implemented")]
    public void Exception_handling_is_applied_for_all_convention_types() =>
        this.ShouldFail();
}