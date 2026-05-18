using Baked.Buildtime.Diagnostics;
using Baked.Ui;

namespace Baked.Test.Ui;

public class ManipulatingSchemaLists : TestSpec
{
    List<IOrderableSchema> _contents = default!;

    public override void SetUp()
    {
        base.SetUp();

        _contents =
        [
            MockMe.AnOrderableSchema(key: "A"),
            MockMe.AnOrderableSchema(key: "B"),
            MockMe.AnOrderableSchema(key: "C")
        ];
    }

    [TestCase("A")]
    [TestCase("B")]
    [TestCase("C")]
    public void Moving_a_schema_to_top(string target)
    {
        _contents.Move(target, toTop: true);

        _contents[0].Key.ShouldBe(target, customMessage: _contents.Select(c => c.Key).Join(" => "));
    }

    [TestCase("A")]
    [TestCase("B")]
    [TestCase("C")]
    public void Moving_a_schema_to_bottom(string target)
    {
        _contents.Move(target, toBottom: true);

        _contents[2].Key.ShouldBe(target, customMessage: _contents.Select(c => c.Key).Join(" => "));
    }

    [TestCase("A", "B", 0)]
    [TestCase("A", "C", 1)]
    [TestCase("B", "A", 0)]
    [TestCase("B", "C", 1)]
    [TestCase("C", "A", 0)]
    [TestCase("C", "B", 1)]
    public void Moving_a_schema_before_another(string target, string before, int expectedIndex)
    {
        _contents.Move(target, before: before);

        _contents[expectedIndex].Key.ShouldBe(target, customMessage: _contents.Select(c => c.Key).Join(" => "));
    }

    [TestCase("A", "B", 1)]
    [TestCase("A", "C", 2)]
    [TestCase("B", "A", 1)]
    [TestCase("B", "C", 2)]
    [TestCase("C", "A", 1)]
    [TestCase("C", "B", 2)]
    public void Moving_a_schema_after_another(string target, string after, int expectedIndex)
    {
        _contents.Move(target, after: after);

        _contents[expectedIndex].Key.ShouldBe(target, customMessage: _contents.Select(c => c.Key).Join(" => "));
    }

    [Test]
    public void Throws_diagnostic_error_when_key_is_not_found_on_get()
    {
        var action = () => _contents.Get("D");

        action.ShouldThrow<DiagnosticException>().Code.ShouldBe(DiagnosticCode.MissingItem);
    }

    [Test]
    public void Throws_diagnostic_error_when_key_is_not_found_on_move()
    {
        var action = () => _contents.Move("D", 1);

        action.ShouldThrow<DiagnosticException>().Code.ShouldBe(DiagnosticCode.MissingItem);
    }

    [Test]
    public void Throws_diagnostic_error_when_key_is_not_found_on_move_before()
    {
        var action = () => _contents.Move("C", before: "D");

        action.ShouldThrow<DiagnosticException>().Code.ShouldBe(DiagnosticCode.MissingItem);
    }

    [Test]
    public void Throws_diagnostic_error_when_key_is_not_found_on_move_after()
    {
        var action = () => _contents.Move("C", after: "D");

        action.ShouldThrow<DiagnosticException>().Code.ShouldBe(DiagnosticCode.MissingItem);
    }
}