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
        options.ConventionOrderMatrix.Bases.Add("B1");
        options.ConventionOrderMatrix.Bases.Add("B2");
        options.ConventionOrderMatrix.Levels.Add("L1");
        options.ConventionOrderMatrix.Levels.Add("L2");
        options.ConventionOrderMatrix.Extensions.Add("E1");
        options.ConventionOrderMatrix.Extensions.Add("E2");

        options.ConventionOrderMatrix.FallbackBase = _ => "B1";
        options.ConventionOrderMatrix.FallbackLevel = _ => "L1";
        options.ConventionOrderMatrix.FallbackExtension = _ => "E1";

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
            }
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
            }
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        _values[0].ShouldBe("C2");
        _values[1].ShouldBe("C1");
    }

    [Test]
    public void Conventions_can_have_Order_and_apply_order_respects_base_extension_and_level_indexes()
    {
        var builder = GiveMe.ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("D"), order: Order.At.WithBase("B1").WithLevel("L2").WithExtension("E2"));
                c.Add(new TestConvention("C"), order: Order.At.WithBase("B1").WithLevel("L1").WithExtension("E2"));
                c.Add(new TestConvention("B"), order: Order.At.WithBase("B1").WithLevel("L2").WithExtension("E1"));
                c.Add(new TestConvention("A"), order: Order.At.WithBase("B1").WithLevel("L1").WithExtension("E1"));

                c.Add(new TestConvention("H"), order: Order.At.WithBase("B2").WithLevel("L2").WithExtension("E2"));
                c.Add(new TestConvention("G"), order: Order.At.WithBase("B2").WithLevel("L1").WithExtension("E2"));
                c.Add(new TestConvention("F"), order: Order.At.WithBase("B2").WithLevel("L2").WithExtension("E1"));
                c.Add(new TestConvention("E"), order: Order.At.WithBase("B2").WithLevel("L1").WithExtension("E1"));
            },
            options: BuildOptions
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        _values[0].ShouldBe("A");
        _values[1].ShouldBe("B");
        _values[2].ShouldBe("C");
        _values[3].ShouldBe("D");
        _values[4].ShouldBe("E");
        _values[5].ShouldBe("F");
        _values[6].ShouldBe("G");
        _values[7].ShouldBe("H");
    }

    [Test]
    public void Conventions_can_define_base_only_order__level_and_extension_are_fallback_values()
    {
        var builder = GiveMe.ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("B"), order: Order.At.WithBase("B2"));
                c.Add(new TestConvention("A"), order: Order.At.WithBase("B1"));
            },
            options: BuildOptions
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        _values[0].ShouldBe("A");
        _values[1].ShouldBe("B");
    }

    [Test]
    public void Conventions_can_define_level_only_order__base_and_extension_are_fallback_values()
    {
        var builder = GiveMe.ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("B"), order: Order.At.WithLevel("L2"));
                c.Add(new TestConvention("A"), order: Order.At.WithLevel("L1"));
            },
            options: BuildOptions
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        _values[0].ShouldBe("A");
        _values[1].ShouldBe("B");
    }

    [Test]
    public void Conventions_can_define_extension_only_order__base_and_level_are_fallback_values()
    {
        var builder = GiveMe.ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("B"), order: Order.At.WithExtension("E2"));
                c.Add(new TestConvention("A"), order: Order.At.WithExtension("E1"));
            },
            options: BuildOptions
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        _values[0].ShouldBe("A");
        _values[1].ShouldBe("B");
    }

    [Test]
    public void Skips_convention_when_level_does_not_exist()
    {
        var messages = new List<DiagnosticMessage>();
        var builder = GiveMe.ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("A"), order: Order.At.WithLevel("NA"));
                c.Add(new TestConvention("B"), order: Order.At.Zero);
            },
            options: BuildOptions,
            onConventionsFinalized: r => messages.AddRange(r.Messages)
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        _values.Count.ShouldBe(1);
        _values[0].ShouldBe("B");
        messages.Count.ShouldBe(1);
        messages.Single().Message.ShouldStartWith($"Convention '{typeof(TestConvention).FullName}' is skipped due to unrecognized order: 'BNE+0000'.");
    }
}