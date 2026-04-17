using Baked.Domain.Configuration;
using System.Reflection;

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
                when: c => c.Type.Is<string>(),
                attribute: () => throw new("test")
            );
            builder.Conventions.AddTypeAttribute(
                when: c => c.Type.Is<string>(),
                attribute: () =>
                {
                    hit = true;

                    return new CustomAttribute();
                }
            );
        });

        builder
            .StartBuild([typeof(string)])
            .EndBuild();

        hit.ShouldBeTrue();
    }

    [Test]
    public void After_post_build__domain_model_builder_delegates_reported_error_to_configured_error_handler()
    {
        var errors = new List<Exception>();
        var builder = GiveMe.ADomainModelBuilder(options: builder =>
        {
            builder.Conventions.AddTypeAttribute(
                when: c => c.Type.Is<string>(),
                attribute: () => throw new("test")
            );
            builder.OnComplete(e => errors.AddRange(e.Errors));
        });

        builder
            .StartBuild([typeof(string)])
            .EndBuild();

        errors.Count.ShouldBe(1);
        errors.ShouldContain(e => e.Message == "test");
    }

    [Test]
    public void It_allows_multiple_errors_in_one_execution()
    {
        var errors = new List<Exception>();
        var builder = GiveMe.ADomainModelBuilder(options: builder =>
        {
            builder.Conventions.AddTypeAttribute(
                when: c => c.Type.Is<string>(),
                attribute: () => throw new("string error")
            );
            builder.Conventions.AddTypeAttribute(
                when: c => c.Type.Is<int>(),
                attribute: () => throw new("int error")
            );
            builder.OnComplete(e => errors.AddRange(e.Errors));
        });

        builder
            .StartBuild([typeof(string), typeof(int)])
            .EndBuild();

        errors.Count.ShouldBe(2);
        errors.ShouldContain(e => e.Message == "string error");
        errors.ShouldContain(e => e.Message == "int error");
    }

    [Test]
    public void It_continues_execution_for_the_same_convention_per_domain_member()
    {
        var errors = new List<Exception>();
        var builder = GiveMe.ADomainModelBuilder(options: builder =>
        {
            builder.Conventions.AddTypeAttribute(
                when: c => c.Type.Is<string>() || c.Type.Is<int>(),
                attribute: c => throw new($"{c.Type.Name} error")
            );
            builder.OnComplete(e => errors.AddRange(e.Errors));
        });

        builder
            .StartBuild([typeof(string), typeof(int)])
            .EndBuild();

        errors.Count.ShouldBe(2);
        errors.ShouldContain(e => e.Message == "String error");
        errors.ShouldContain(e => e.Message == "Int32 error");
    }

    class StubConvention :
        IDomainModelConvention<TypeModelContext>,
        IDomainModelConvention<TypeModelGenericsContext>,
        IDomainModelConvention<TypeModelInheritanceContext>,
        IDomainModelConvention<TypeModelMetadataContext>,
        IDomainModelConvention<TypeModelMembersContext>,
        IDomainModelConvention<PropertyModelContext>,
        IDomainModelConvention<MethodModelContext>,
        IDomainModelConvention<ParameterModelContext>
    {
        public void Apply(TypeModelContext model) => throw new("basics");
        public void Apply(TypeModelGenericsContext model) => throw new("generics");
        public void Apply(TypeModelInheritanceContext model) => throw new("inheritance");
        public void Apply(TypeModelMetadataContext model) => throw new("metadata");
        public void Apply(TypeModelMembersContext model) => throw new("members");
        public void Apply(PropertyModelContext model) => throw new("property");
        public void Apply(MethodModelContext model) => throw new("method");
        public void Apply(ParameterModelContext model) => throw new("parameter");
    }

    [Test]
    public void Exception_handling_is_applied_for_all_convention_types()
    {
        var errors = new List<Exception>();
        var builder = GiveMe.ADomainModelBuilder(options: builder =>
        {
            builder.BuildLevels.Clear();
            builder.BuildLevels.Add(t => t == typeof(string), BuildLevels.Members);
            builder.BuildLevels.Add(t => t == typeof(long), BuildLevels.Metadata);
            builder.BuildLevels.Add(t => t == typeof(double), BuildLevels.Inheritance);
            builder.BuildLevels.Add(t => t == typeof(int), BuildLevels.Generics);
            builder.BuildLevels.Add(BuildLevels.Basics);
            builder.BindingFlags.Property = BindingFlags.Instance | BindingFlags.Public;
            builder.BindingFlags.Method = BindingFlags.Instance | BindingFlags.Public;
            builder.Conventions.Add(new StubConvention());
            builder.OnComplete(e => errors.AddRange(e.Errors));
        });

        builder
            .StartBuild([typeof(char), typeof(int), typeof(double), typeof(long), typeof(string)])
            .EndBuild();

        errors.ShouldContain(e => e.Message == "basics");
        errors.ShouldContain(e => e.Message == "generics");
        errors.ShouldContain(e => e.Message == "inheritance");
        errors.ShouldContain(e => e.Message == "metadata");
        errors.ShouldContain(e => e.Message == "members");
        errors.ShouldContain(e => e.Message == "property");
        errors.ShouldContain(e => e.Message == "method");
        errors.ShouldContain(e => e.Message == "parameter");
    }
}