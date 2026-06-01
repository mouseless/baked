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
    public void Conventions_can_have_level_based_orders()
    {
        var builder = GiveMe.ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("B"), order: Order.At.WithLevel("B"));
                c.Add(new TestConvention("A"), order: Order.At.WithLevel("A"));
            },
            options: o =>
            {
                o.ConventionLevels.Add("A");
                o.ConventionLevels.Add("B");
            }
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
                c.Add(new TestConvention("B"), order: Order.At.WithLevel("B"));
                c.Add(new TestConvention("B"), order: Order.At.Zero);
            },
            options: o =>
            {
                o.ConventionLevels.Add("A");
                o.DefaultConventionLevel = "A";
            },
            onConventionsFinalized: r => messages.AddRange(r.Messages)
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        messages.Count.ShouldBe(1);
        messages.Single().Message.ShouldBe("Given level 'B' was not found in configured levels, defaulting to 'A'");
    }
}