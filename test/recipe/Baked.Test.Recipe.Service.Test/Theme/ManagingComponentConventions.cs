using Baked.Test.Theme;
using Baked.Theme.Admin;
using Baked.Ui;

namespace Baked.Test.Test;

public class ManagingComponentConventions : TestServiceSpec
{
    [Test]
    public void Adding_component_to_a_type()
    {
        var type = GiveMe.TheTypeModel<TestPage>().GetMetadata();
        var componentContext = GiveMe.AComponentContext(path: "/page");

        var component = type.GetRequiredComponent(componentContext);

        var reportPage = component.ShouldBeOfType<ComponentDescriptor<ReportPage>>();
        reportPage.Schema.Path.ShouldBe("test-page");
        reportPage.Schema.Title.Title.ShouldBe("Test Page");
    }

    [Test]
    public void Adding_schema_to_a_type()
    {
        var type = GiveMe.TheTypeModel<TestPage>().GetMetadata();
        var componentContext = GiveMe.AComponentContext(path: "/page/tabs");

        var tabs = type.GetSchemas<ReportPage.Tab>(componentContext);

        tabs.Count.ShouldBe(1);
        tabs[0].Id.ShouldBe("default");
    }

    [Test]
    public void Adding_schema_to_a_method()
    {
        var type = GiveMe.TheTypeModel<TestPage>().GetMembers();
        var method = type.Methods[nameof(TestPage.GetData)];
        var componentContext = GiveMe.AComponentContext(path: "/page/tabs/default/contents/0");

        var content = method.GetSchema<ReportPage.Tab.Content>(componentContext);

        content.ShouldNotBeNull();
        content.Narrow.GetValueOrDefault().ShouldBeTrue();
    }

    [Test]
    public void Adding_convention_to_a_method()
    {
        var type = GiveMe.TheTypeModel<TestPage>().GetMembers();
        var method = type.Methods[nameof(TestPage.GetData)];
        var componentContext = GiveMe.AComponentContext(path: "/page/tabs/default/contents/0/component");

        var component = method.GetRequiredComponent(componentContext);

        var @string = component.ShouldBeOfType<ComponentDescriptor<Baked.Theme.Admin.String>>();
        @string.Schema.MaxLength.ShouldBe(20);
    }
}