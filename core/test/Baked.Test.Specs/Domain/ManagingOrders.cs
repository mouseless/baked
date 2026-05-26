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
        Order.Create.Global.Calculate(_levels, "B").ShouldBe(0);
        Order.Create.Global.AbsoluteMin.Calculate(_levels, "B").ShouldBe(int.MinValue);
        Order.Create.Global.Min.Calculate(_levels, "B").ShouldBe(int.MinValue + 10);
        Order.Create.Global.Max.Calculate(_levels, "B").ShouldBe(int.MaxValue - 10);
        Order.Create.Global.AbsoluteMax.Calculate(_levels, "B").ShouldBe(int.MaxValue);

        // Pre Level
        Order.Create.Level("A").Calculate(_levels, "B").ShouldBe(-10000);
        Order.Create.Level("A").AbsoluteMin.Calculate(_levels, "B").ShouldBe(-15000);
        Order.Create.Level("A").Min.Calculate(_levels, "B").ShouldBe(-14990);
        Order.Create.Level("A").Max.Calculate(_levels, "B").ShouldBe(-5011);
        Order.Create.Level("A").AbsoluteMax.Calculate(_levels, "B").ShouldBe(-5001);

        // Default Level
        Order.Create.Level("B").Calculate(_levels, "B").ShouldBe(0);
        Order.Create.Level("B").AbsoluteMin.Calculate(_levels, "B").ShouldBe(-5000);
        Order.Create.Level("B").Min.Calculate(_levels, "B").ShouldBe(-4990);
        Order.Create.Level("B").Max.Calculate(_levels, "B").ShouldBe(4989);
        Order.Create.Level("B").AbsoluteMax.Calculate(_levels, "B").ShouldBe(4999);

        // Post Level
        Order.Create.Level("C").Calculate(_levels, "B").ShouldBe(10000);
        Order.Create.Level("C").AbsoluteMin.Calculate(_levels, "B").ShouldBe(5000);
        Order.Create.Level("C").Min.Calculate(_levels, "B").ShouldBe(5010);
        Order.Create.Level("C").Max.Calculate(_levels, "B").ShouldBe(14989);
        Order.Create.Level("C").AbsoluteMax.Calculate(_levels, "B").ShouldBe(14999);

        // Default with no level
        Order.Create.Calculate(_levels, "B").ShouldBe(0);
        Order.Create.AbsoluteMin.Calculate(_levels, "B").ShouldBe(-5000);
        Order.Create.Min.Calculate(_levels, "B").ShouldBe(-4990);
        Order.Create.Max.Calculate(_levels, "B").ShouldBe(4989);
        Order.Create.AbsoluteMax.Calculate(_levels, "B").ShouldBe(4999);
    }

    [Test]
    public void Order_can_be_created_from_int_as_order_with_offset()
    {
        var order = (Order)10;

        order.Calculate(_levels, "B").ShouldBe(10);
    }

    [Test]
    public void Order_cannot_be_below_absolute_min_value()
    {
        var order = Order.Create.Level("A").AbsoluteMin - 1;

        var action = () => { order.Calculate(_levels, "B"); };

        var exception = action.ShouldThrow<DiagnosticException>();
        exception.Code.ShouldBe(DiagnosticCode.OrderOutOfBounds);
        exception.Message.ShouldBe($"Order offset (-5001) must be between -5000 - 4999");
    }

    [Test]
    public void Order_cannot_exceed_absolute_max_value()
    {
        var order = Order.Create.Level("A").AbsoluteMax + 1;

        var action = () => { order.Calculate(_levels, "B"); };

        var exception = action.ShouldThrow<DiagnosticException>();
        exception.Code.ShouldBe(DiagnosticCode.OrderOutOfBounds);
        exception.Message.ShouldBe($"Order offset (5000) must be between -5000 - 4999");
    }

    [Test]
    public void Throws_exception_when_default_layer_is_not_defined()
    {
        var order = Order.Create.Level("A");

        var action = () => { order.Calculate(_levels, "not-existing"); };

        var exception = action.ShouldThrow<DiagnosticException>();
        exception.Code.ShouldBe(DiagnosticCode.UndefinedLevel);
        exception.Message.ShouldBe($"Default level (not-existing) must be defined in levels");
    }
}