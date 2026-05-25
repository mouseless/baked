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
        var builder = GiveMe.ADomainModelBuilder(conventions: conventions =>
        {
            conventions.SetTypeAttribute(
                when: c => c.Type.Is<string>(),
                attribute: () => throw GiveMe.ADiagnosticCode().Exception(GiveMe.AString())
            );
            conventions.SetTypeAttribute(
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
        var exceptions = new List<Exception>();
        var builder = GiveMe.ADomainModelBuilder(
            options: builder =>
            {
                builder.OnComplete = e => exceptions.AddRange(e.Exceptions);
            },
            conventions: conventions =>
            {
                conventions.SetTypeAttribute(
                    when: c => c.Type.Is<string>(),
                    attribute: () => throw GiveMe.ADiagnosticCode().Exception("test")
                );
            }
        );

        builder
            .StartBuild([typeof(string)])
            .EndBuild();

        exceptions.Count.ShouldBe(1);
        exceptions.ShouldContain(e => e.Message == "test");
    }

    [Test]
    public void It_allows_multiple_errors_in_one_execution()
    {
        var exceptions = new List<Exception>();
        var builder = GiveMe.ADomainModelBuilder(
            options: builder =>
            {
                builder.OnComplete = e => exceptions.AddRange(e.Exceptions);
            },
            conventions: conventions =>
            {
                conventions.SetTypeAttribute(
                    when: c => c.Type.Is<string>(),
                    attribute: () => throw GiveMe.ADiagnosticCode().Exception("string error")
                );
                conventions.SetTypeAttribute(
                    when: c => c.Type.Is<int>(),
                    attribute: () => throw GiveMe.ADiagnosticCode().Exception("int error")
                );
            }
        );

        builder
            .StartBuild([typeof(string), typeof(int)])
            .EndBuild();

        exceptions.Count.ShouldBe(2);
        exceptions.ShouldContain(e => e.Message == "string error");
        exceptions.ShouldContain(e => e.Message == "int error");
    }

    [Test]
    public void It_continues_execution_for_the_same_convention_per_domain_member()
    {
        var exceptions = new List<Exception>();
        var builder = GiveMe.ADomainModelBuilder(
            options: builder =>
            {
                builder.OnComplete = e => exceptions.AddRange(e.Exceptions);
            }, conventions: conventions =>
            {
                conventions.SetTypeAttribute(
                    when: c => c.Type.Is<string>() || c.Type.Is<int>(),
                    attribute: c => throw GiveMe.ADiagnosticCode().Exception($"{c.Type.Name} error")
                );
            }
        );

        builder
            .StartBuild([typeof(string), typeof(int)])
            .EndBuild();

        exceptions.Count.ShouldBe(2);
        exceptions.ShouldContain(e => e.Message == "String error");
        exceptions.ShouldContain(e => e.Message == "Int32 error");
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
        public void Apply(TypeModelContext model) =>
            throw giveMe.ADiagnosticCode().Exception("basics");

        public void Apply(TypeModelGenericsContext model) =>
            throw giveMe.ADiagnosticCode().Exception("generics");

        public void Apply(TypeModelInheritanceContext model) =>
            throw giveMe.ADiagnosticCode().Exception("inheritance");

        public void Apply(TypeModelMetadataContext model) =>
            throw giveMe.ADiagnosticCode().Exception("metadata");

        public void Apply(TypeModelMembersContext model) =>
            throw giveMe.ADiagnosticCode().Exception("members");

        public void Apply(PropertyModelContext model) =>
            throw giveMe.ADiagnosticCode().Exception("property");

        public void Apply(MethodModelContext model) =>
            throw giveMe.ADiagnosticCode().Exception("method");

        public void Apply(ParameterModelContext model) =>
            throw giveMe.ADiagnosticCode().Exception("parameter");
    }

    [Test]
    public void Exception_handling_is_applied_for_all_convention_types()
    {
        var exceptions = new List<Exception>();
        var builder = GiveMe.ADomainModelBuilder(
            options: builder =>
            {
                builder.BuildLevels.Clear();
                builder.BuildLevels.Add(t => t == typeof(string), BuildLevels.Members);
                builder.BuildLevels.Add(t => t == typeof(long), BuildLevels.Metadata);
                builder.BuildLevels.Add(t => t == typeof(double), BuildLevels.Inheritance);
                builder.BuildLevels.Add(t => t == typeof(int), BuildLevels.Generics);
                builder.BuildLevels.Add(BuildLevels.Basics);
                builder.BindingFlags.Property = BindingFlags.Instance | BindingFlags.Public;
                builder.BindingFlags.Method = BindingFlags.Instance | BindingFlags.Public;
                builder.OnComplete = e => exceptions.AddRange(e.Exceptions);
            },
            conventions: conventions =>
            {
                conventions.Add(new StubConvention(GiveMe));
            }
        );

        builder
            .StartBuild([typeof(char), typeof(int), typeof(double), typeof(long), typeof(string)])
            .EndBuild();

        exceptions.ShouldContain(e => e.Message == "basics");
        exceptions.ShouldContain(e => e.Message == "generics");
        exceptions.ShouldContain(e => e.Message == "inheritance");
        exceptions.ShouldContain(e => e.Message == "metadata");
        exceptions.ShouldContain(e => e.Message == "members");
        exceptions.ShouldContain(e => e.Message == "property");
        exceptions.ShouldContain(e => e.Message == "method");
        exceptions.ShouldContain(e => e.Message == "parameter");
    }
}