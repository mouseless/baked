using Baked.Domain;
using Baked.Domain.Configuration;

namespace Baked.Test.Domain;

[TestFixture]
public class ApplyingConventions
{
    static List<string> _values = default!;

    static DomainModelBuilder ADomainModelBuilder(
            Action<DomainModelBuilderOptions>? options = default,
            Action<IDomainModelConventionCollection>? conventions = default
        )
    {
        var optionsInstance = new DomainModelBuilderOptions();
        optionsInstance.BuildLevels.Add(BuildLevels.Metadata);
        optionsInstance.OnComplete = _ => { };

        if (options is not null)
        {
            options(optionsInstance);
        }

        var conventionsInstance = new DomainModelConventionCollection(optionsInstance);
        if (conventions is not null)
        {
            conventions(conventionsInstance);
        }

        return new DomainModelBuilder(optionsInstance, conventionsInstance);
    }

    [SetUp]
    public void SetUp()
    {
        _values = new();
    }

    [TearDown]
    public void TearDown()
    {
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
        var builder = ADomainModelBuilder(
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
        var builder = ADomainModelBuilder(
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
        var builder = ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("B"), order: Order.FromLevel("B"));
                c.Add(new TestConvention("A"), order: Order.FromLevel("A"));
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
    public void Order_level_is_set_to_default_when_not_specified()
    {
        var builder = ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("B"), order: Order.Default);
                c.Add(new TestConvention("A"), order: Order.FromLevel("A"));
            },
            options: o =>
            {
                o.ConventionLevels.Add("A");
                o.ConventionLevels.Add("B");
                o.DefaultLevel = "B";
            }
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        _values[0].ShouldBe("A");
        _values[1].ShouldBe("B");
    }

    [Test]
    public void Levels_have_default__min_and_max_values()
    {
        var builder = ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("C2"), order: Order.FromLevel("A").Min);
                c.Add(new TestConvention("C3"), order: Order.FromLevel("A").Max);
                c.Add(new TestConvention("C1"), order: Order.FromLevel("A"));
            },
            options: o =>
            {
                o.ConventionLevels.Add("A");
            }
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        _values[0].ShouldBe("C2");
        _values[1].ShouldBe("C1");
        _values[2].ShouldBe("C3");
    }

    [Test]
    public void Levels_does_not_intersect()
    {
        var builder = ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("A"), order: Order.FromLevel("A").AbsoluteMax);
                c.Add(new TestConvention("B"), order: Order.FromLevel("B").AbsoluteMin);
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
    public void Order_cannot_exceed_level_absolute_max_value()
    {
        var exceptions = new List<Exception>();
        var builder = ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("A"), order: Order.FromLevel("A").AbsoluteMax + 1);
                c.Add(new TestConvention("A"), order: Order.FromLevel("A").Max + 11);
            },
            options: o =>
            {
                o.ConventionLevels.Add("A");
                o.OnComplete = e => exceptions.AddRange(e.Exceptions);
            }
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        exceptions.Count.ShouldBe(2);
        exceptions.ShouldAllBe(e => e.Message == "Order cannot exceed allowed absolute max value");
    }

    [Test]
    public void Order_cannot_be_lover_than_absolute_min_value()
    {
        var exceptions = new List<Exception>();
        var builder = ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("A"), order: Order.FromLevel("A").AbsoluteMin - 1);
                c.Add(new TestConvention("A"), order: Order.FromLevel("A").Min - 11);
            },
            options: o =>
            {
                o.ConventionLevels.Add("A");
                o.OnComplete = e => exceptions.AddRange(e.Exceptions);
            }
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        exceptions.Count.ShouldBe(2);
        exceptions.ShouldAllBe(e => e.Message == "Order cannot be lower than allowed absolute min value");
    }

    [Test]
    public void Global_order_has_same_range_with_int()
    {
        var exceptions = new List<Exception>();
        var builder = ADomainModelBuilder(
            conventions: c =>
            {
                c.Add(new TestConvention("A"), order: Order.Global + int.MinValue);
                c.Add(new TestConvention("A"), order: Order.Global + int.MaxValue);
            },
            options: o =>
            {
                o.OnComplete = e => exceptions.AddRange(e.Exceptions);
            }
        );
        var postBuilder = builder.StartBuild([typeof(string)]);
        postBuilder.EndBuild();

        exceptions.Count.ShouldBe(0);
    }
}