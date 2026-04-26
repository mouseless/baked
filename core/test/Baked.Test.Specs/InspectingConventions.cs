using Baked.Playground.Ui;
using Baked.Theme;
using Baked.Ui;
using System.Text.RegularExpressions;

using static Baked.Ui.Datas;

using B = Baked.Ui.Components;
using C = Baked.Playground.Ui.Components;

namespace Baked.Test;

public class InspectingConventions : TestSpec
{
    readonly List<DiagnosticMessage> _messages = [];
    InspectTrace _inspect = Inspect.TraceHere();
    IDisposable? _diagnostics;

    public override void SetUp()
    {
        base.SetUp();

        _diagnostics = Diagnostics.Start(GiveMe.AString(), result => _messages.AddRange(result.Messages));
    }

    public override void TearDown()
    {
        base.TearDown();

        _diagnostics?.Dispose();
        _messages.Clear();
    }

    [Test]
    public void When_a_schema_is_created_with_a_non_null_on_the_inspected_property__it_reports_schema_path_and_the_initial_value()
    {
        Inspect.Schema<DataTable.Column>(dtc => dtc.Title);
        var cc = GiveMe.AComponentContext(paths: ["test", "path"]);

        using (_diagnostics)
        {
            _inspect.Capture(cc, () => B.DataTableColumn(GiveMe.AString(), options: dtc => dtc.Title = "test title"));
        }

        _messages.Count.ShouldBe(2);
        _messages[0].Level.ShouldBe("info");
        _messages[0].Message.ShouldContain("/test/path");
        _messages[1].Level.ShouldBe("info");
        _messages[1].Message.ShouldContain($"test title");
    }

    [Test]
    public void When_the_inspected_property_of_a_schema_is_updated__it_reports_only_if_new_value_is_different()
    {
        Inspect.Schema<DataTable.Column>(dtc => dtc.Title);
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            var dtc = B.DataTableColumn(key: GiveMe.AString(), options: dtc => dtc.Title = "initial");

            _inspect.Capture(cc, dtc, () => dtc.Title = "updated");
            _inspect.Capture(cc, dtc, () => dtc.Title = "updated");
        }

        _messages.Count(c => c.Message.Contains("updated")).ShouldBe(1);
    }

    [Test]
    public void Capture_returns_the_expected_schema__so_that_usages_can_return_with_a_single_line()
    {
        Inspect.Schema<DataTable.Column>(dtc => dtc.Title);
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            var dtc = _inspect.Capture(cc, () => B.DataTableColumn(GiveMe.AString(), options: t => t.Title = "test title"));

            dtc.Title.ShouldBe("test title");
        }
    }

    [Test]
    public void It_prints_schema_path_once_for_consequent_updates()
    {
        Inspect.Schema<DataTable.Column>(dtc => dtc.Title);
        var cc = GiveMe.AComponentContext(paths: ["test", "path"]);

        using (_diagnostics)
        {
            var dtc = _inspect.Capture(cc, () => B.DataTableColumn(key: GiveMe.AString(), options: dtc => dtc.Title = "1"));

            _inspect.Capture(cc, dtc, () => dtc.Title = "2");
        }

        _messages.Count(c => c.Message.Contains("/test/path")).ShouldBe(1);
    }

    [Test]
    public void Capture_does_not_report_when_a_non_inspected_property_is_set_or_updated()
    {
        Inspect.Schema<DataTable.Column>(dtc => dtc.Title);
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            var dtc = _inspect.Capture(cc, () => B.DataTableColumn(GiveMe.AString(), options: dtc => dtc.AlignRight = true));
            _inspect.Capture(cc, dtc, () => dtc.AlignRight = false);
        }

        _messages.Count(c => c.Message.Contains($"{true}")).ShouldBe(0);
    }

    [Test]
    public void Allows_inspecting_a_component_property()
    {
        Inspect.Component<Text>(t => t.Prop);
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            _inspect.Capture(cc, () => B.Text(options: t => t.Prop = "testProp"));
        }

        _messages.ShouldContain(m => m.Message.Contains("testProp"));
    }

    [Test]
    public void Allows_inspection_on_interfaces()
    {
        Inspect.Component<ISelect>(s => s.OptionLabel);
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            var sb = _inspect.Capture(cc, () => B.SelectButton(
                data: Inline(new[] { new { testProp = GiveMe.AString() } }),
                options: sb => sb.OptionLabel = "initialized")
            );
            _inspect.Capture(cc, sb, () => sb.Schema.OptionLabel = "updated");
        }

        _messages.ShouldContain(m => m.Message.Contains("initialized"));
        _messages.ShouldContain(m => m.Message.Contains("updated"));
    }

    [Test]
    public void Allows_inspection_on_component_overrides()
    {
        Inspect.Component<MyText>(mt => mt.SomethingExtra);
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            var t = B.Text();
            _inspect.Capture(cc, t, () => t.Override(C.MyText(mt => mt.SomethingExtra = "overridden")));
        }

        _messages.ShouldContain(m => m.Message.Contains("overridden"));
    }

    [Test]
    [Ignore("not tested")]
    public void Allows_inspecting_an_attribute_property() =>
        this.ShouldFail();

    [Test]
    public void Filters_by_component_context()
    {
        Inspect
            .Where(cc => cc.Path.StartsWith("page-1"))
            .Component<Text>(t => t.Prop)
        ;
        var page1 = GiveMe.AComponentContext(paths: ["page-1"]);
        var page2 = GiveMe.AComponentContext(paths: ["page-2"]);

        using (_diagnostics)
        {
            _inspect.Capture(page1, () => B.Text(options: t => t.Prop = "prop1"));
            _inspect.Capture(page2, () => B.Text(options: t => t.Prop = "prop2"));
        }

        _messages.ShouldContain(m => m.Message.Contains("prop1"));
        _messages.ShouldNotContain(m => m.Message.Contains("prop2"));
    }

    [Test]
    [Ignore("not tested")]
    public void Filters_by_model_context() =>
        this.ShouldFail();

    [Test]
    public void Reports_path_in_gray_for_readability()
    {
        Inspect.Component<Text>(t => t.Prop);
        var cc = GiveMe.AComponentContext(paths: ["test", "path"]);

        using (_diagnostics)
        {
            _inspect.Capture(cc, () => B.Text(options: t => t.Prop = GiveMe.AString()));
        }

        _messages.ShouldContain(m => m.Message.Contains("[gray]/test/path[/]"));
    }

    [Test]
    public void Reports_schema_type_and_property_name_for_components()
    {
        Inspect.Schema<DataTable.Column>(dtc => dtc.Title);
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            _inspect.Capture(cc, () => B.DataTableColumn(GiveMe.AString(), options: dtc => dtc.Title = "test"));
        }

        _messages.ShouldContain(m => m.Message.Contains("<DataTable.Column>"));
        _messages.ShouldContain(m => m.Message.Contains("[gray]Title:[/] test"));
    }

    [Test]
    public void Reports_component_type_and_property_name_for_components()
    {
        Inspect.Component<DataTable>(dt => dt.Paginator);
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            _inspect.Capture(cc, () => B.DataTable(options: dt => dt.Paginator = true));
        }

        _messages.ShouldContain(m => m.Message.Contains("<DataTable>"));
        _messages.ShouldContain(m => m.Message.Contains("[gray]Paginator:[/] True"));
    }

    [Test]
    [Ignore("not tested")]
    public void Reports_attribute_type_and_property_name_for_attributes() =>
        this.ShouldFail();

    [Test]
    public void Captures_and_reports_feature_name_from_stack_trace()
    {
        Inspect.Schema<DataTable.Column>(dtc => dtc.Title);

        using (_diagnostics)
        {
            new StubUxFeature(GiveMe).Configure(() =>
                B.DataTableColumn(GiveMe.AString(), options: dtc => dtc.Title = GiveMe.AString())
            );
        }

        _messages.ShouldContain(m => Regex.IsMatch(m.Message, @"\[link=.*]StubUxFeature:\d+\[/]"));
    }

    [Test]
    public void Reports_the_whole_stack_trace_when_feature_is_not_captured()
    {
        Inspect.Schema<DataTable.Column>(dtc => dtc.Title);
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            _inspect.Capture(cc, () => B.DataTableColumn(GiveMe.AString(), options: dtc => dtc.Title = "test"));
        }

        _messages.ShouldContain(m => m.Message.Contains("[magenta]<unknown>[/]"));
        _messages.ShouldContain(m =>
            Regex.IsMatch(m.Message, @"\[gray].*at Baked[.]Test[.]InspectingConventions[.][.]ctor\(\).*\[/]",
                RegexOptions.Singleline
            )
        );
    }

    [Test]
    public void Reports_new_value_as_json_when_value_is_not_value_type_or_string()
    {
        Inspect.Component<Text>(c => c);
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            _inspect.Capture(cc, () => B.Text(options: t => t.Prop = "test"));
        }

        _messages.ShouldContain(m => m.Message.Contains($$"""
        {
          "MaxLength": null,
          "Prop": "test"
        }
        """), customMessage: _messages.Join(", "));
    }
}