using Baked.Buildtime.Diagnostics;
using Baked.Domain.Configuration;
using Baked.Testing;

namespace Baked.Test.Domain;

[TestFixture]
public class ApplyingConventions : Spec
{
    static List<string> _values = default!;

    public override void SetUp()
    {
        base.SetUp();

        _values = new();
    }

    public override void TearDown()
    {
        base.TearDown();

        _values.Clear();
    }

    void BuildOptions(DomainModelBuilderOptions options)
    {
        options.ConventionMatrix.Bases.Add("B1");
        options.ConventionMatrix.Bases.Add("B2");
        options.ConventionMatrix.Levels.Add("L1");
        options.ConventionMatrix.Levels.Add("L2");
        options.ConventionMatrix.Extensions.Add("E1");
        options.ConventionMatrix.Extensions.Add("E2");

        options.ConventionMatrix.FallbackBase = _ => "B1";
        options.ConventionMatrix.FallbackLevel = _ => "L1";
        options.ConventionMatrix.FallbackExtension = _ => "E1";

        options.DefaultConventionLevel = "B1.L1.E1";
    }

    public class TestConvention(string _value) : IDomainModelConvention<TypeModelContext>
    {
        public void Apply(TypeModelContext context)
        {
            if (!context.Type.Is<string>()) { return; }

            _values.Add(_value);
        }
    }

    [Test]
    public void Conventions_are_applied_based_on_added_order()
    {
        var builder = GiveMe.ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("C1"));
                c.Add(new TestConvention("C2"));
            },
            conventionMatrixDefaults: false,
            options: BuildOptions
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        _values[0].ShouldBe("C1");
        _values[1].ShouldBe("C2");
    }

    [Test]
    public void Conventions_can_have_specific_orders()
    {
        var builder = GiveMe.ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("C1"), order: 2);
                c.Add(new TestConvention("C2"), order: 1);
            },
            conventionMatrixDefaults: false,
            options: BuildOptions
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        _values[0].ShouldBe("C2");
        _values[1].ShouldBe("C1");
    }

    [Test]
    public void Conventions_can_have_bases()
    {
        var builder = GiveMe.ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("B"), order: Order.At.WithBase("B2"));
                c.Add(new TestConvention("A"), order: Order.At.WithBase("B1"));
            },
            conventionMatrixDefaults: false,
            options: BuildOptions
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        _values[0].ShouldBe("A");
        _values[1].ShouldBe("B");
    }

    [Test]
    public void Conventions_can_have_level_based_orders()
    {
        var builder = GiveMe.ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("B"), order: Order.At.WithLevel("L2"));
                c.Add(new TestConvention("A"), order: Order.At.WithLevel("L1"));
            },
            conventionMatrixDefaults: false,
            options: BuildOptions
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        _values[0].ShouldBe("A");
        _values[1].ShouldBe("B");
    }

    [Test]
    public void Conventions_can_have_level_extension_orders()
    {
        var builder = GiveMe.ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("B"), order: Order.At.WithExtension("E2"));
                c.Add(new TestConvention("A"), order: Order.At.WithExtension("E1"));
            },
            conventionMatrixDefaults: false,
            options: BuildOptions
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        _values[0].ShouldBe("A");
        _values[1].ShouldBe("B");
    }

    [Test]
    public void Prompts_diagnostic_warning_on_default_layer_fallback()
    {
        var messages = new List<DiagnosticMessage>();
        var builder = GiveMe.ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("B"), order: Order.At.WithLevel("not existing"));
                c.Add(new TestConvention("B"), order: Order.At.Zero);
            },
            conventionMatrixDefaults: false,
            options: BuildOptions,
            onConventionsFinalized: r => messages.AddRange(r.Messages)
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        messages.Count.ShouldBe(1);
        messages.Single().Message.ShouldBe("Given level 'not existing' was not found in configured levels, defaulting to 'B1.L1.E1'");
    }
}