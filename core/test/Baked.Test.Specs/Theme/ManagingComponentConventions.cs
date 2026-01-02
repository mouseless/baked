using Baked.Playground.Theme;
using Baked.Ui;

namespace Baked.Test.Theme;

public class ManagingComponentConventions : TestSpec
{
    [Test]
    public void Adding_component_to_a_type()
    {
        var type = GiveMe.TheTypeModel<TestPage>().GetMetadata();
        var componentContext = GiveMe.AComponentContext(paths: ["page"]);

        var component = type.GetRequiredComponent(componentContext);

        var page = component.ShouldBeOfType<ComponentDescriptor<TabbedPage>>();
        page.Schema.Path.ShouldBe("test-page");
        page.Schema.Title.Title.ShouldBe("Test Page");
    }

    [Test]
    public void Adding_schema_to_a_type()
    {
        var type = GiveMe.TheTypeModel<TestPage>().GetMetadata();
        var componentContext = GiveMe.AComponentContext(paths: ["page", "tabs"]);

        var tabs = type.GetSchemas<Tab>(componentContext);

        tabs.Count.ShouldBe(1);
        tabs[0].Id.ShouldBe("default");
    }

    [Test]
    public void Adding_schema_to_a_method()
    {
        var type = GiveMe.TheTypeModel<TestPage>().GetMembers();
        var method = type.Methods[nameof(TestPage.GetData)];
        var componentContext = GiveMe.AComponentContext(paths: ["page", "tabs", "default", "contents", "0"]);

        var content = method.GetSchema<Content>(componentContext);

        content.ShouldNotBeNull();
        content.Narrow.GetValueOrDefault().ShouldBeTrue();
    }

    [Test]
    public void Adding_convention_to_a_method()
    {
        var type = GiveMe.TheTypeModel<TestPage>().GetMembers();
        var method = type.Methods[nameof(TestPage.GetData)];
        var componentContext = GiveMe.AComponentContext(paths: ["page", "tabs", "default", "contents", "0", "component"]);

        var component = method.GetRequiredComponent(componentContext);

        var @string = component.ShouldBeOfType<ComponentDescriptor<Text>>();
        @string.Schema.MaxLength.ShouldBe(20);
    }
}