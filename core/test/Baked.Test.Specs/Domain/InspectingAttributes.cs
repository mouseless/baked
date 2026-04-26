using Baked.Business;
using Baked.Domain.Configuration;
using Baked.Playground.Orm;
using Baked.Theme;
using Baked.Theme.Default;

namespace Baked.Test;

public class InspectingAttributes : TestSpec
{
    readonly List<DiagnosticMessage> _messages = [];
    InspectTrace _trace = Inspect.TraceHere();
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

    IEnumerable<(DomainModelContext, string)> CreateContextCases()
    {
        var domain = GiveMe.TheDomainModel();
        var parent = GiveMe.TheTypeModel<Parent>().GetMembers();
        var name = parent.Properties[nameof(Parent.Name)];
        var addChild = parent.Methods[nameof(Parent.AddChild)];
        var pName = addChild.DefaultOverload.Parameters["name"];

        yield return (new TypeModelContext { Domain = domain, Type = parent }, "Baked.Playground.Orm.Parent");
        yield return (new TypeModelGenericsContext { Domain = domain, Type = parent }, "Baked.Playground.Orm.Parent");
        yield return (new TypeModelInheritanceContext { Domain = domain, Type = parent }, "Baked.Playground.Orm.Parent");
        yield return (new TypeModelMetadataContext { Domain = domain, Type = parent }, "Baked.Playground.Orm.Parent");
        yield return (new TypeModelMembersContext { Domain = domain, Type = parent }, "Baked.Playground.Orm.Parent");
        yield return
            (
                new PropertyModelContext
                {
                    Domain = domain,
                    Type = parent,
                    Property = name
                },
                $"Baked.Playground.Orm.Parent.{nameof(Parent.Name)}"
            );
        yield return
            (
                new MethodModelContext
                {
                    Domain = domain,
                    Type = parent,
                    Method = addChild
                },
                $"Baked.Playground.Orm.Parent.{nameof(Parent.AddChild)}"
            );
        yield return
            (
                new ParameterModelContext
                {
                    Domain = domain,
                    Type = parent,
                    Method = addChild,
                    MethodOverload = addChild.DefaultOverload,
                    Parameter = pName
                },
                $"Baked.Playground.Orm.Parent.{nameof(Parent.AddChild)}.name"
            );
    }

    [Test]
    public void When_an_attribute_is_added_with_a_non_null_on_the_inspected_property__it_reports_applied_member_and_the_initial_value()
    {
        Inspect.Attribute<DataAttribute>(d => d.Label);

        var cases = CreateContextCases();
        using (_diagnostics)
        {
            foreach (var (context, memberName) in cases)
            {
                _trace.Capture(context, () => new DataAttribute("test") { Label = "Test" });
            }
        }

        _messages.Count.ShouldBe(2 * cases.Count());
        var i = 0;
        foreach (var (context, memberName) in cases)
        {
            _messages[0 + i * 2].Level.ShouldBe("info");
            _messages[0 + i * 2].Message.ShouldContain(memberName);
            _messages[1 + i * 2].Level.ShouldBe("info");
            _messages[1 + i * 2].Message.ShouldContain($"Test");

            i++;
        }
    }

    [Test]
    public void Allows_attribute_adding_without_any_property()
    {
        Inspect.Attribute<LabelAttribute>();
        var context = GiveMe.ATypeModelContext<Parent>();

        var cases = CreateContextCases();
        using (_diagnostics)
        {
            _trace.Capture(context, () => new LabelAttribute());
        }

        _messages.ShouldContain(m => m.Message.Contains("[gray]<this>:[/] {}"));
    }

    [Test]
    [Ignore("not implemented")]
    public void When_the_inspected_property_of_an_attribute_is_updated__it_reports_only_if_new_value_is_different()
    {
        this.ShouldFail();
        // Inspect.Schema<DataTable.Column>(dtc => dtc.Title);
        // var cc = GiveMe.AComponentContext();
        //
        // using (_diagnostics)
        // {
        //     var dtc = B.DataTableColumn(key: GiveMe.AString(), options: dtc => dtc.Title = "initial");
        //
        //     _trace.Capture(cc, dtc, () => dtc.Title = "updated");
        //     _trace.Capture(cc, dtc, () => dtc.Title = "updated");
        // }
        //
        // _messages.Count(c => c.Message.Contains("updated")).ShouldBe(1);
    }

    [Test]
    [Ignore("not implemented")]
    public void Capture_returns_the_expected_attribute__so_that_usages_can_return_with_a_single_line()
    {
        this.ShouldFail();
        // Inspect.Schema<DataTable.Column>(dtc => dtc.Title);
        // var cc = GiveMe.AComponentContext();
        //
        // using (_diagnostics)
        // {
        //     var dtc = _trace.Capture(cc, () => B.DataTableColumn(GiveMe.AString(), options: t => t.Title = "test title"));
        //
        //     dtc.Title.ShouldBe("test title");
        // }
    }

    [Test]
    [Ignore("not implemented")]
    public void It_prints_member_name_once_for_consequent_updates()
    {
        this.ShouldFail();
        // Inspect.Schema<DataTable.Column>(dtc => dtc.Title);
        // var cc = GiveMe.AComponentContext(paths: ["test", "path"]);
        //
        // using (_diagnostics)
        // {
        //     var dtc = _trace.Capture(cc, () => B.DataTableColumn(key: GiveMe.AString(), options: dtc => dtc.Title = "1"));
        //
        //     _trace.Capture(cc, dtc, () => dtc.Title = "2");
        // }
        //
        // _messages.Count(c => c.Message.Contains("/test/path")).ShouldBe(1);
    }

    [Test]
    [Ignore("not implemented")]
    public void Capture_does_not_report_when_a_non_inspected_property_is_set_or_updated()
    {
        this.ShouldFail();
        // Inspect.Schema<DataTable.Column>(dtc => dtc.Title);
        // var cc = GiveMe.AComponentContext();
        //
        // using (_diagnostics)
        // {
        //     var dtc = _trace.Capture(cc, () => B.DataTableColumn(GiveMe.AString(), options: dtc => dtc.AlignRight = true));
        //     _trace.Capture(cc, dtc, () => dtc.AlignRight = false);
        // }
        //
        // _messages.Count(c => c.Message.Contains($"{true}")).ShouldBe(0);
    }

    [Test]
    [Ignore("not implemented")]
    public void Allows_inspection_on_interfaces()
    {
        this.ShouldFail();
        // Inspect.Component<ISelect>(s => s.OptionLabel);
        // var cc = GiveMe.AComponentContext();
        //
        // using (_diagnostics)
        // {
        //     var sb = _trace.Capture(cc, () => B.SelectButton(
        //         data: Inline(new[] { new { testProp = GiveMe.AString() } }),
        //         options: sb => sb.OptionLabel = "initialized")
        //     );
        //     _trace.Capture(cc, sb, () => sb.Schema.OptionLabel = "updated");
        // }
        //
        // _messages.ShouldContain(m => m.Message.Contains("initialized"));
        // _messages.ShouldContain(m => m.Message.Contains("updated"));
    }

    [Test]
    [Ignore("not tested")]
    public void Filters_by_model_context() =>
        this.ShouldFail();

    [Test]
    [Ignore("not tested")]
    public void Reports_member_in_gray_for_readability()
    {
        this.ShouldFail();
        // Inspect.Component<Text>(t => t.Prop);
        // var cc = GiveMe.AComponentContext(paths: ["test", "path"]);
        //
        // using (_diagnostics)
        // {
        //     _trace.Capture(cc, () => B.Text(options: t => t.Prop = GiveMe.AString()));
        // }
        //
        // _messages.ShouldContain(m => m.Message.Contains("[gray]/test/path[/]"));
    }

    [Test]
    public void Reports_attribute_type_and_property_name()
    {
        Inspect.Attribute<DataAttribute>(d => d.Label);
        var c = new TypeModelContext { Domain = GiveMe.TheDomainModel(), Type = GiveMe.TheTypeModel<Parent>() };

        using (_diagnostics)
        {
            _trace.Capture(c, () => new DataAttribute("test") { Label = "Test" });
        }

        _messages.ShouldContain(m => m.Message.Contains("[[Data]]"));
        _messages.ShouldContain(m => m.Message.Contains("[gray]Label:[/] Test"));
    }

    [Test]
    [Ignore("not tested")]
    public void Captures_and_reports_feature_name_from_stack_trace()
    {
        this.ShouldFail();
        // Inspect.Schema<DataTable.Column>(dtc => dtc.Title);
        //
        // using (_diagnostics)
        // {
        //     new StubUxFeature(GiveMe).Configure(() =>
        //         B.DataTableColumn(GiveMe.AString(), options: dtc => dtc.Title = GiveMe.AString())
        //     );
        // }
        //
        // _messages.ShouldContain(m => Regex.IsMatch(m.Message, @"\[link=.*]StubUxFeature:\d+\[/]"));
    }

    [Test]
    [Ignore("not tested")]
    public void Reports_the_whole_stack_trace_when_feature_is_not_captured()
    {
        this.ShouldFail();
        // Inspect.Schema<DataTable.Column>(dtc => dtc.Title);
        // var cc = GiveMe.AComponentContext();
        //
        // using (_diagnostics)
        // {
        //     _trace.Capture(cc, () => B.DataTableColumn(GiveMe.AString(), options: dtc => dtc.Title = "test"));
        // }
        //
        // _messages.ShouldContain(m => m.Message.Contains("[magenta]<unknown>[/]"));
        // _messages.ShouldContain(m =>
        //     Regex.IsMatch(m.Message, @"\[gray].*at Baked[.]Test[.]InspectingAttributes[.][.]ctor\(\).*\[/]",
        //         RegexOptions.Singleline
        //     )
        // );
    }

    [Test]
    [Ignore("not tested")]
    public void Reports_new_value_as_json_when_value_is_not_value_type_or_string()
    {
        this.ShouldFail();
        // Inspect.Component<Text>(c => c);
        // var cc = GiveMe.AComponentContext();
        //
        // using (_diagnostics)
        // {
        //     _trace.Capture(cc, () => B.Text(options: t => t.Prop = "test"));
        // }
        //
        // _messages.ShouldContain(m => m.Message.Contains($$"""
        // {
        //   "MaxLength": null,
        //   "Prop": "test"
        // }
        // """), customMessage: _messages.Join(", "));
    }
}