using Baked.Buildtime.Diagnostics;
using Baked.Domain.Configuration;

namespace Baked.Test.Domain;

[TestFixture]
public class ManagingOrders
{
    ICollection<string> _levelCollection = ["A", "B", "C"];
    IReadOnlyDictionary<string, int> _levels = default!;
    IDisposable _diagnostics = default!;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _levels = _levelCollection.Select((name, index) => new { name, index })
            .ToDictionary(x => x.name, x => x.index);
    }

    [SetUp]
    public void Setup() =>
        _diagnostics = Diagnostics.Start(nameof(ManagingOrders));

    [TearDown]
    public void TearDown() =>
        _diagnostics.Dispose();

    [Test]
    public void Usages()
    {
        // Global Level
        Order.At.Global.AbsoluteMin.Calculate(_levels, "B").ShouldBe(int.MinValue);
        Order.At.Global.Min.Calculate(_levels, "B").ShouldBe(int.MinValue + 10);

        // Pre Level
        Order.At.Level("A").AbsoluteMin.Calculate(_levels, "B").ShouldBe(-15000);
        Order.At.Level("A").Min.Calculate(_levels, "B").ShouldBe(-14990);
        Order.At.Level("A").Calculate(_levels, "B").ShouldBe(-10000);
        Order.At.Level("A").Max.Calculate(_levels, "B").ShouldBe(-5011);
        Order.At.Level("A").AbsoluteMax.Calculate(_levels, "B").ShouldBe(-5001);

        // Default Level
        Order.At.Level("B").AbsoluteMin.Calculate(_levels, "B").ShouldBe(-5000);
        Order.At.AbsoluteMin.Calculate(_levels, "B").ShouldBe(-5000);
        Order.At.Level("B").Min.Calculate(_levels, "B").ShouldBe(-4990);
        Order.At.Min.Calculate(_levels, "B").ShouldBe(-4990);

        Order.At.Global.Calculate(_levels, "B").ShouldBe(0);
        Order.At.Level("B").Calculate(_levels, "B").ShouldBe(0);
        Order.At.Zero.Calculate(_levels, "B").ShouldBe(0);

        Order.At.Level("B").Max.Calculate(_levels, "B").ShouldBe(4989);
        Order.At.Max.Calculate(_levels, "B").ShouldBe(4989);
        Order.At.Level("B").AbsoluteMax.Calculate(_levels, "B").ShouldBe(4999);
        Order.At.AbsoluteMax.Calculate(_levels, "B").ShouldBe(4999);

        // Post Level
        Order.At.Level("C").AbsoluteMin.Calculate(_levels, "B").ShouldBe(5000);
        Order.At.Level("C").Min.Calculate(_levels, "B").ShouldBe(5010);
        Order.At.Level("C").Calculate(_levels, "B").ShouldBe(10000);
        Order.At.Level("C").Max.Calculate(_levels, "B").ShouldBe(14989);
        Order.At.Level("C").AbsoluteMax.Calculate(_levels, "B").ShouldBe(14999);

        // Global Level
        Order.At.Global.Max.Calculate(_levels, "B").ShouldBe(int.MaxValue - 10);
        Order.At.Global.AbsoluteMax.Calculate(_levels, "B").ShouldBe(int.MaxValue);
    }

    [Test]
    public void Order_can_be_created_from_int_as_order_with_offset()
    {
        Order order = 10;

        order.Calculate(_levels, "B").ShouldBe(10);
    }

    [Test]
    public void Setting_level_defaults_global_flag_to_false()
    {
        var order = Order.At.Global.Level("B").AbsoluteMin;

        order.Calculate(_levels, "B").ShouldBe(-5000);
    }

    [Test]
    public void A_level_change_can_be_set_as_default_so_that_it_is_used_when_a_level_is_not_present()
    {
        Order.At.Level("C", @default: true).Calculate(_levels, "B").ShouldBe(10000);
        Order.At.Level("B").Level("C", @default: true).Calculate(_levels, "B").ShouldBe(0);
    }

    [Test]
    public void Order_cannot_be_below_absolute_min_value()
    {
        var order = Order.At.Level("A").AbsoluteMin - 1;

        var action = () => { order.Calculate(_levels, "B"); };

        var exception = action.ShouldThrow<DiagnosticException>();
        exception.Code.ShouldBe(DiagnosticCode.OrderOutOfBounds);
        exception.Message.ShouldBe("Order (A: -5001) must be between -5000 - 4999");
    }

    [Test]
    public void Order_cannot_exceed_absolute_max_value()
    {
        var order = Order.At.Level("A").AbsoluteMax + 1;

        var action = () => { order.Calculate(_levels, "B"); };

        var exception = action.ShouldThrow<DiagnosticException>();
        exception.Code.ShouldBe(DiagnosticCode.OrderOutOfBounds);
        exception.Message.ShouldBe("Order (A: 5000) must be between -5000 - 4999");
    }

    [Test]
    public void Throws_exception_when_default_layer_is_not_defined()
    {
        var order = Order.At.Level("A");

        var action = () => { order.Calculate(_levels, "not-existing"); };

        var exception = action.ShouldThrow<DiagnosticException>();
        exception.Code.ShouldBe(DiagnosticCode.UndefinedLevel);
        exception.Message.ShouldBe($"Default level (not-existing) must be defined in levels");
    }
}