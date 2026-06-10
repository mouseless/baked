using Baked.Buildtime.Diagnostics;
using Baked.Domain.Configuration;
using Baked.Domain.Inspection;
using Baked.Playground.Orm;
using Baked.Playground.Ui;
using Baked.Ui;

using static Baked.Ui.Datas;

using B = Baked.Ui.Components;
using C = Baked.Playground.Ui.Components;

namespace Baked.Test.Theme;

public class InspectingComponentAndSchemas : TestSpec
{
    readonly List<DiagnosticMessage> _messages = [];
    readonly Trace _trace = Trace.Here();
    Inspect _inspect = default!;
    IDisposable? _diagnostics;

    public override void SetUp()
    {
        base.SetUp();

        _inspect = new();
        _diagnostics = Diagnostics.Start(GiveMe.AString(), result => _messages.AddRange(result.Messages));
    }

    public override void TearDown()
    {
        base.TearDown();

        _diagnostics?.Dispose();
        _messages.Clear();
        Inspection.Clear();
    }

    [Test]
    public void Allows_inspecting_a_schema_property()
    {
        _inspect.Schema<DataTable.Column>(
            schema: dtc => dtc.Title
        );
        var c = GiveMe.ATypeModelContext<Parent>();
        var cc = GiveMe.AComponentContext(paths: ["test", "path"]);

        using (_diagnostics)
        {
            _trace.CaptureDescriptor(c, cc, () => B.DataTableColumn(GiveMe.AString(), options: dtc => dtc.Title = "test title"));
        }

        _messages.Count.ShouldBe(2);
        _messages[0].Level.ShouldBe("info");
        _messages[0].Message.ShouldContain("/test/path");
        _messages[1].Level.ShouldBe("info");
        _messages[1].Message.ShouldContain($"test title");
    }

    [Test]
    public void Allows_inspecting_a_component_property()
    {
        _inspect.Component<Text>(
            component: t => t.Prop
        );
        var c = GiveMe.ATypeModelContext<Parent>();
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            _trace.CaptureDescriptor(c, cc, () => B.Text(options: t => t.Prop = "testProp"));
        }

        _messages.ShouldContain(m => m.Message.Contains("testProp"));
    }

    [Test]
    public void Allows_inspection_on_interfaces()
    {
        _inspect.Component<ISelect>(
            component: s => s.OptionLabel
        );
        var c = GiveMe.ATypeModelContext<Parent>();
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            var sb = _trace.CaptureDescriptor(c, cc, () => B.SelectButton(
                data: Inline(new[] { new { testProp = GiveMe.AString() } }),
                options: sb => sb.OptionLabel = "initialized")
            );
            _trace.CaptureDescriptor(c, cc, sb, () => sb.Schema.OptionLabel = "updated");
        }

        _messages.ShouldContain(m => m.Message.Contains("initialized"));
        _messages.ShouldContain(m => m.Message.Contains("updated"));
    }

    [Test]
    public void Allows_inspection_on_component_overrides()
    {
        _inspect.Component<MyText>(
            component: mt => mt.SomethingExtra
        );
        var c = GiveMe.ATypeModelContext<Parent>();
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            var t = B.Text();
            _trace.CaptureDescriptor(c, cc, t, () => t.Override(C.MyText(mt => mt.SomethingExtra = "overridden")));
        }

        _messages.ShouldContain(m => m.Message.Contains("overridden"));
    }

    [Test]
    public void Allows_inspecting_a_schema_without_any_property()
    {
        _inspect.Schema<DataTable.Column>(
            schema: dtc => new
            {
                dtc.Key,
                Component = dtc.Component.Type
            }
        );
        var c = GiveMe.ATypeModelContext<Parent>();
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            _trace.CaptureDescriptor(c, cc, () => B.DataTableColumn("test-key"));
        }

        _messages.ShouldContain(m => m.Message.Contains("""
        [darkgoldenrod]Key = dtc.Key, Component = dtc.Component.Type:[/] {
          "key": "test-key",
          "component": "Text"
        }
        """), customMessage: _messages.Join(", "));
    }

    [Test]
    public void Allows_inspecting_a_component_without_any_property()
    {
        _inspect.Component<Text>();
        var c = GiveMe.ATypeModelContext<Parent>();
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            _trace.CaptureDescriptor(c, cc, () => B.Text());
        }

        _messages.ShouldContain(m => m.Message.Contains("[darkgoldenrod]<self>:[/] Text"));
    }

    [Test]
    public void Capture_returns_the_expected_schema__so_that_usages_can_return_with_a_single_line()
    {
        _inspect.Schema<DataTable.Column>(
            schema: dtc => dtc.Title
        );
        var c = GiveMe.ATypeModelContext<Parent>();
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            var dtc = _trace.CaptureDescriptor(c, cc, () => B.DataTableColumn(GiveMe.AString(), options: t => t.Title = "test title"));

            dtc.Title.ShouldBe("test title");
        }
    }

    [Test]
    public void It_prints_component_path_once_for_consequent_updates()
    {
        _inspect.Schema<DataTable.Column>(
            schema: dtc => dtc.Title
        );
        var c = GiveMe.ATypeModelContext<Parent>();
        var cc = GiveMe.AComponentContext(paths: ["test", "path"]);

        using (_diagnostics)
        {
            var dtc = _trace.CaptureDescriptor(c, cc, () => B.DataTableColumn(key: GiveMe.AString(), options: dtc => dtc.Title = "1"));

            _trace.CaptureDescriptor(c, cc, dtc, () => dtc.Title = "2");
        }

        _messages.Count(c => c.Message.Contains("/test/path")).ShouldBe(1);
    }

    [Test]
    public void Provides_where_filter_to_filter_by_component_context()
    {
        _inspect.Component<Text>(
            where: cc => cc.Path.StartsWith("page-1"),
            component: t => t.Prop
        );
        var c = GiveMe.ATypeModelContext<Parent>();
        var page1 = GiveMe.AComponentContext(paths: ["page-1"]);
        var page2 = GiveMe.AComponentContext(paths: ["page-2"]);

        using (_diagnostics)
        {
            _trace.CaptureDescriptor(c, page1, () => B.Text(options: t => t.Prop = "prop1"));
            _trace.CaptureDescriptor(c, page2, () => B.Text(options: t => t.Prop = "prop2"));
        }

        _messages.ShouldContain(m => m.Message.Contains("prop1"));
        _messages.ShouldNotContain(m => m.Message.Contains("prop2"));
    }

    [Test]
    public void Provides_when_filter_to_filter_by_type_model_context()
    {
        _inspect.TypeComponent<Text>(
            when: c => c.Type.Is<Parent>()
        );
        var domain = GiveMe.TheDomainModel();
        var cParent = new TypeModelMetadataContext { Domain = domain, Type = domain.Types[typeof(Parent)].GetMetadata() };
        var cChild = new TypeModelMetadataContext { Domain = domain, Type = domain.Types[typeof(Child)].GetMetadata() };
        var ccParent = GiveMe.AComponentContext(paths: ["page", "parent"]);
        var ccChild = GiveMe.AComponentContext(paths: ["page", "child"]);

        using (_diagnostics)
        {
            _trace.CaptureDescriptor(cParent, ccParent, () => B.Text());
            _trace.CaptureDescriptor(cChild, ccChild, () => B.Text());
        }

        _messages.ShouldContain(m => m.Message.Contains("/page/parent"));
        _messages.ShouldNotContain(m => m.Message.Contains("/page/child"));
    }

    [Test]
    public void Provides_when_filter_to_filter_by_property_model_context()
    {
        _inspect.PropertyComponent<Text>(
            when: c => c.Property.Name == nameof(Parent.Id)
        );
        var domain = GiveMe.TheDomainModel();
        var parent = domain.Types[typeof(Parent)].GetMembers();
        var cId = new PropertyModelContext { Domain = domain, Type = parent, Property = parent.Properties[nameof(Parent.Id)] };
        var cName = new PropertyModelContext { Domain = domain, Type = parent, Property = parent.Properties[nameof(Parent.Name)] };
        var ccId = GiveMe.AComponentContext(paths: ["page", "parent", "id"]);
        var ccName = GiveMe.AComponentContext(paths: ["page", "parent", "name"]);

        using (_diagnostics)
        {
            _trace.CaptureDescriptor(cId, ccId, () => B.Text());
            _trace.CaptureDescriptor(cName, ccName, () => B.Text());
        }

        _messages.ShouldContain(m => m.Message.Contains("/page/parent/id"));
        _messages.ShouldNotContain(m => m.Message.Contains("/page/parent/name"));
    }

    [Test]
    public void Provides_when_filter_to_filter_by_method_model_context()
    {
        _inspect.MethodComponent<Text>(
            when: c => c.Method.Name == nameof(Parent.AddChild)
        );
        var domain = GiveMe.TheDomainModel();
        var parent = domain.Types[typeof(Parent)].GetMembers();
        var cAddChild = new MethodModelContext { Domain = domain, Type = parent, Method = parent.Methods[nameof(Parent.AddChild)] };
        var cGetChildren = new MethodModelContext { Domain = domain, Type = parent, Method = parent.Methods[nameof(Parent.GetChildren)] };
        var ccAddChild = GiveMe.AComponentContext(paths: ["page", "parent", "add-child"]);
        var ccGetChildren = GiveMe.AComponentContext(paths: ["page", "parent", "get-children"]);

        using (_diagnostics)
        {
            _trace.CaptureDescriptor(cAddChild, ccAddChild, () => B.Text());
            _trace.CaptureDescriptor(cGetChildren, ccGetChildren, () => B.Text());
        }

        _messages.ShouldContain(m => m.Message.Contains("/page/parent/add-child"));
        _messages.ShouldNotContain(m => m.Message.Contains("/page/parent/get-children"));
    }

    [Test]
    public void Provides_when_filter_to_filter_by_parameter_model_context()
    {
        _inspect.ParameterComponent<Text>(
            when: c => c.Parameter.Name == "name"
        );
        var domain = GiveMe.TheDomainModel();
        var parent = domain.Types[typeof(Parent)].GetMembers();
        var update = parent.Methods[nameof(Parent.Update)];
        var cName = new ParameterModelContext
        {
            Domain = domain,
            Type = parent,
            Method = update,
            MethodOverload = update.DefaultOverload,
            Parameter = update.DefaultOverload.Parameters["name"]
        };
        var cSurname = new ParameterModelContext
        {
            Domain = domain,
            Type = parent,
            Method = parent.Methods[nameof(Parent.Update)],
            MethodOverload = update.DefaultOverload,
            Parameter = update.DefaultOverload.Parameters["surname"]
        };
        var ccName = GiveMe.AComponentContext(paths: ["page", "parent", "update", "name"]);
        var ccSurname = GiveMe.AComponentContext(paths: ["page", "parent", "update", "surname"]);

        using (_diagnostics)
        {
            _trace.CaptureDescriptor(cName, ccName, () => B.Text());
            _trace.CaptureDescriptor(cSurname, ccSurname, () => B.Text());
        }

        _messages.ShouldContain(m => m.Message.Contains("/page/parent/update/name"));
        _messages.ShouldNotContain(m => m.Message.Contains("/page/parent/update/surname"));
    }

    [Test]
    public void Reports_path_in_magenta_for_readability()
    {
        _inspect.Component<Text>();
        var c = GiveMe.ATypeModelContext<Parent>();
        var cc = GiveMe.AComponentContext(paths: ["test", "path"]);

        using (_diagnostics)
        {
            _trace.CaptureDescriptor(c, cc, () => B.Text());
        }

        _messages.ShouldContain(m => m.Message.Contains("[magenta]/test/path[/]"));
    }

    [Test]
    public void Reports_schema_type_and_property_name_for_components()
    {
        _inspect.Schema<DataTable.Column>(
            schema: dtc => dtc.Title
        );
        var c = GiveMe.ATypeModelContext<Parent>();
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            _trace.CaptureDescriptor(c, cc, () => B.DataTableColumn(GiveMe.AString(), options: dtc => dtc.Title = "test"));
        }

        _messages.ShouldContain(m => m.Message.Contains("<DataTable.Column>"));
        _messages.ShouldContain(m => m.Message.Contains("[darkgoldenrod]Title:[/] test"));
    }

    [Test]
    public void Reports_component_type_and_property_name_for_components()
    {
        _inspect.Component<DataTable>(
            component: dt => dt.Paginator
        );
        var c = GiveMe.ATypeModelContext<Parent>();
        var cc = GiveMe.AComponentContext();

        using (_diagnostics)
        {
            _trace.CaptureDescriptor(c, cc, () => B.DataTable(options: dt => dt.Paginator = true));
        }

        _messages.ShouldContain(m => m.Message.Contains("<DataTable>"));
        _messages.ShouldContain(m => m.Message.Contains("[darkgoldenrod]Paginator:[/] True"));
    }
}