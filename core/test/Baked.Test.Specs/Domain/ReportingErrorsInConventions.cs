using Baked.Domain.Configuration;
using Baked.Testing;
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
            builder.Conventions.SetTypeAttribute(
                when: c => c.Type.Is<string>(),
                attribute: () => throw GiveMe.ADiagnosticsCode().Exception(GiveMe.AString())
            );
            builder.Conventions.SetTypeAttribute(
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
            builder.Conventions.SetTypeAttribute(
                when: c => c.Type.Is<string>(),
                attribute: () => throw GiveMe.ADiagnosticsCode().Exception("test")
            );
            builder.OnComplete = e => errors.AddRange(e.Errors);
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
            builder.Conventions.SetTypeAttribute(
                when: c => c.Type.Is<string>(),
                attribute: () => throw GiveMe.ADiagnosticsCode().Exception("string error")
            );
            builder.Conventions.SetTypeAttribute(
                when: c => c.Type.Is<int>(),
                attribute: () => throw GiveMe.ADiagnosticsCode().Exception("int error")
            );
            builder.OnComplete = e => errors.AddRange(e.Errors);
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
            builder.Conventions.SetTypeAttribute(
                when: c => c.Type.Is<string>() || c.Type.Is<int>(),
                attribute: c => throw GiveMe.ADiagnosticsCode().Exception($"{c.Type.Name} error")
            );
            builder.OnComplete = e => errors.AddRange(e.Errors);
        });

        builder
            .StartBuild([typeof(string), typeof(int)])
            .EndBuild();

        errors.Count.ShouldBe(2);
        errors.ShouldContain(e => e.Message == "String error");
        errors.ShouldContain(e => e.Message == "Int32 error");
    }

    class StubConvention(Stubber giveMe) :
        IDomainModelConvention<TypeModelContext>,
        IDomainModelConvention<TypeModelGenericsContext>,
        IDomainModelConvention<TypeModelInheritanceContext>,
        IDomainModelConvention<TypeModelMetadataContext>,
        IDomainModelConvention<TypeModelMembersContext>,
        IDomainModelConvention<PropertyModelContext>,
        IDomainModelConvention<MethodModelContext>,
        IDomainModelConvention<ParameterModelContext>
    {
        public void Apply(TypeModelContext model) => throw giveMe.ADiagnosticsCode().Exception("basics");
        public void Apply(TypeModelGenericsContext model) => throw giveMe.ADiagnosticsCode().Exception("generics");
        public void Apply(TypeModelInheritanceContext model) => throw giveMe.ADiagnosticsCode().Exception("inheritance");
        public void Apply(TypeModelMetadataContext model) => throw giveMe.ADiagnosticsCode().Exception("metadata");
        public void Apply(TypeModelMembersContext model) => throw giveMe.ADiagnosticsCode().Exception("members");
        public void Apply(PropertyModelContext model) => throw giveMe.ADiagnosticsCode().Exception("property");
        public void Apply(MethodModelContext model) => throw giveMe.ADiagnosticsCode().Exception("method");
        public void Apply(ParameterModelContext model) => throw giveMe.ADiagnosticsCode().Exception("parameter");
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
            builder.Conventions.Add(new StubConvention(GiveMe));
            builder.OnComplete = e => errors.AddRange(e.Errors);
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