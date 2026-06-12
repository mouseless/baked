using Baked.Buildtime.Diagnostics;
using Baked.Domain.Configuration;

namespace Baked.Test.Domain;

[TestFixture]
public class ManagingOrders
{
    ICollection<string> _levelCollection = [
        "BaseA.LevelA.ExtA",
        "BaseB.LevelB.ExtB",
        "BaseC.LevelC.ExtC",
    ];
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
        var orderA = Order.At.WithBase("BaseA").WithLevel("LevelA").WithExtension("ExtA");
        var orderB = Order.At.WithBase("BaseB").WithLevel("LevelB").WithExtension("ExtB");
        var orderC = Order.At.WithBase("BaseC").WithLevel("LevelC").WithExtension("ExtC");

        // Global Level
        Order.At.Global.AbsoluteMin.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(int.MinValue);
        Order.At.Global.Min.Calculate(_levels, "BaseB.LevelB.Ext.B").ShouldBe(int.MinValue + 10);

        // Pre Level
        orderA.AbsoluteMin.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(-15000);
        orderA.Min.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(-14990);
        orderA.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(-10000);
        orderA.Max.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(-5011);
        orderA.AbsoluteMax.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(-5001);

        // Default Level
        orderB.AbsoluteMin.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(-5000);
        orderB.Min.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(-4990);

        Order.At.Global.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(0);

        orderB.Max.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(4989);
        orderB.AbsoluteMax.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(4999);

        // Post Level
        orderC.AbsoluteMin.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(5000);
        orderC.Min.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(5010);
        orderC.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(10000);
        orderC.Max.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(14989);
        orderC.AbsoluteMax.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(14999);

        // Global Level
        Order.At.Global.Max.Calculate(_levels, "BaseB.LevelB.Ext.B").ShouldBe(int.MaxValue - 10);
        Order.At.Global.AbsoluteMax.Calculate(_levels, "BaseB.LevelB.Ext.B").ShouldBe(int.MaxValue);
    }

    [Test]
    public void Order_value_is_calculated_relative_to_default_level()
    {
        var orderA = Order.At.WithBase("BaseA").WithLevel("LevelA").WithExtension("ExtA");
        var orderB = Order.At.WithBase("BaseB").WithLevel("LevelB").WithExtension("ExtB");
        var orderC = Order.At.WithBase("BaseC").WithLevel("LevelC").WithExtension("ExtC");

        orderA.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(-10000);
        orderB.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(0);
        orderC.Calculate(_levels, "BaseB.LevelB.ExtB").ShouldBe(10000);
    }

    [Test]
    public void Order_can_be_created_from_int_as_order_with_offset()
    {
        Order order = 10;
        order = order
            .WithBase("BaseA")
            .WithLevel("LevelA")
            .WithExtension("ExtA");

        order.Calculate(_levels, "BaseA.LevelA.ExtA").ShouldBe(10);
    }

    [Test]
    public void Order_mutators_have_no_effect_when_global()
    {
        Order order = Order.At.Global.AbsoluteMin;
        order = order
            .WithBase("BaseA")
            .WithLevel("LevelA")
            .WithExtension("ExtA");

        order.Calculate(_levels, "BaseA.LevelA.ExtA").ShouldBe(int.MinValue);
    }

    [Test]
    public void Calculate_throws_exception_when_order_base_is_not_defined()
    {
        var order = Order.At
            .WithLevel("LevelA")
            .WithExtension("ExtA");

        var action = () => { order.Calculate(_levels, "BaseA.LevelA.ExtA"); };

        var exception = action.ShouldThrow<DiagnosticException>();
        exception.Code.ShouldBe(DiagnosticCode.InvalidOrder);
        exception.Message.ShouldBe($"Order 'base' cannot be null");
    }

    [Test]
    public void Calculate_throws_exception_when_order_level_is_not_defined()
    {
        var order = Order.At
            .WithBase("BaseA")
            .WithExtension("ExtA");

        var action = () => { order.Calculate(_levels, "BaseA.LevelA.ExtA"); };

        var exception = action.ShouldThrow<DiagnosticException>();
        exception.Code.ShouldBe(DiagnosticCode.InvalidOrder);
        exception.Message.ShouldBe($"Order 'level' cannot be null");
    }

    [Test]
    public void Calculate_throws_exception_when_order_extension_is_not_defined()
    {
        var order = Order.At
            .WithBase("BaseA")
            .WithLevel("LevelA");

        var action = () => { order.Calculate(_levels, "BaseA.LevelA.ExtA"); };

        var exception = action.ShouldThrow<DiagnosticException>();
        exception.Code.ShouldBe(DiagnosticCode.InvalidOrder);
        exception.Message.ShouldBe($"Order 'extension' cannot be null");
    }

    [Test]
    public void Calculate_throws_exception_when_default_layer_is_not_defined()
    {
        var order = Order.At.WithBase("Base").WithLevel("Level").WithExtension("Extension");

        var action = () => { order.Calculate(_levels, "not-existing"); };

        var exception = action.ShouldThrow<DiagnosticException>();
        exception.Code.ShouldBe(DiagnosticCode.UndefinedLevel);
        exception.Message.ShouldBe($"Default level (not-existing) must be defined in levels");
    }

    [Test]
    public void Calculate_throws_exception_when_offset_is_below_absolute_min_value()
    {
        var order = Order.At
            .WithBase("BaseA")
            .WithLevel("LevelA")
            .WithExtension("ExtA")
            .AbsoluteMin - 1;

        var action = () => { order.Calculate(_levels, "BaseA.LevelA.ExtA"); };

        var exception = action.ShouldThrow<DiagnosticException>();
        exception.Code.ShouldBe(DiagnosticCode.OrderOutOfBounds);
        exception.Message.ShouldBe("Order (BaseA.LevelA.ExtA: -5001) must be between -5000 - 4999");
    }

    [Test]
    public void Calculate_throws_exception_when_offset_exceeds_absolute_max_value()
    {
        var order = Order.At
            .WithBase("BaseA")
            .WithLevel("LevelA")
            .WithExtension("ExtA")
            .AbsoluteMax + 1;

        var action = () => { order.Calculate(_levels, "BaseA.LevelA.ExtA"); };

        var exception = action.ShouldThrow<DiagnosticException>();
        exception.Code.ShouldBe(DiagnosticCode.OrderOutOfBounds);
        exception.Message.ShouldBe("Order (BaseA.LevelA.ExtA: 5000) must be between -5000 - 4999");
    }

    [Test]
    public void To_string_returns_base_level_ext_and_offset_info()
    {
        var order = Order.At
            .WithBase("BaseA")
            .WithLevel("LevelA")
            .WithExtension("ExtA");
        var orderWithPositiveOffset = order + 10;
        var orderWithNegativeOffset = order - 10;

        order.ToString().ShouldBe("BLE+0000");
        orderWithPositiveOffset.ToString().ShouldBe("BLE+0010");
        orderWithNegativeOffset.ToString().ShouldBe("BLE-0010");
    }

    [Test]
    public void ToString_returns_question_mark_for_unknown_order_parts()
    {
        var order = Order.At.WithBase("BaseA");

        order.ToString().ShouldBe("B??+0000");
    }
}