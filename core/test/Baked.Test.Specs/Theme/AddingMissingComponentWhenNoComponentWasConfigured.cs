using Baked.Domain.Model;
using Baked.Test.Business;
using Baked.Test.Theme;
using Baked.Ui;

namespace Baked.Test.Test;

public class AddingMissingComponentWhenNoComponentWasConfigured : TestSpec
{
    TextWriter _realOut = default!;
    TextWriter _fakeOut = default!;

    public override void SetUp()
    {
        base.SetUp();

        _realOut = Console.Out;
        Console.SetOut(_fakeOut = new StringWriter());
    }

    public override void TearDown()
    {
        base.TearDown();

        Console.SetOut(_realOut);
    }

    string ConsoleOutput => _fakeOut?.ToString() ?? string.Empty;

    [Test]
    public void When_no_component_is_found__it_returns_MissingComponent()
    {
        var type = GiveMe.TheTypeModel<TestPage>().GetMetadata();
        var componentContext = GiveMe.AComponentContext(paths: ["page", "with-no-config"]);

        var component = type.GetRequiredComponent(componentContext);

        component.ShouldBeOfType<ComponentDescriptor<MissingComponent>>();
    }

    [Test]
    public void When_used__MissingComponent_leaves_an_error_log_in_build_output()
    {
        var type = GiveMe.TheTypeModel<TestPage>().GetMetadata();
        var componentContext = GiveMe.AComponentContext(paths: ["page", "with-no-config"]);

        type.GetRequiredComponent(componentContext);

        ConsoleOutput.ShouldContainWithoutWhitespace("""
        error: `TestPage` doesn't have any component descriptor at path `/page/with-no-config`
        """);
    }

    [Test]
    public void MissingComponent_contains_component_path_to_build_a_sample_config_code()
    {
        var type = GiveMe.TheTypeModel<TestPage>().GetMetadata();
        var componentContext = GiveMe.AComponentContext(paths: ["page", "with-no-config"]);

        var component = type.GetRequiredComponent(componentContext);

        var missingComponent = component.ShouldBeOfType<ComponentDescriptor<MissingComponent>>();
        missingComponent.Schema.Path.ShouldBe(["page", "with-no-config"]);
    }

    [Test]
    public void When_used_in_a_type__MissingComponent_contains_type_info()
    {
        var type = GiveMe.TheTypeModel<TestPage>().GetMetadata();
        var componentContext = GiveMe.AComponentContext(paths: ["page", "with-no-config"]);

        var component = type.GetRequiredComponent(componentContext);

        var missingComponent = component.ShouldBeOfType<ComponentDescriptor<MissingComponent>>();
        missingComponent.Schema.Source?.Type.ShouldBe(nameof(TypeModelMembers));
        missingComponent.Schema.Source?.Path.ShouldBe([nameof(TestPage)]);
    }

    [Test]
    public void When_used_in_a_property__MissingComponent_contains_property_info()
    {
        var type = GiveMe.TheTypeModel<Record>().GetMembers();
        var property = type.Properties[nameof(Record.Text)];
        var componentContext = GiveMe.AComponentContext(paths: ["page", "with-no-config"]);

        var component = property.GetRequiredComponent(componentContext);

        var missingComponent = component.ShouldBeOfType<ComponentDescriptor<MissingComponent>>();
        missingComponent.Schema.Source?.Type.ShouldBe(nameof(PropertyModel));
        missingComponent.Schema.Source?.Path.ShouldBe([nameof(Record), nameof(Record.Text)]);
    }

    [Test]
    public void When_used_in_a_method__MissingComponent_contains_method_info()
    {
        var type = GiveMe.TheTypeModel<TestPage>().GetMembers();
        var method = type.Methods[nameof(TestPage.GetData)];
        var componentContext = GiveMe.AComponentContext(paths: ["page", "with-no-config"]);

        var component = method.GetRequiredComponent(componentContext);

        var missingComponent = component.ShouldBeOfType<ComponentDescriptor<MissingComponent>>();
        missingComponent.Schema.Source?.Type.ShouldBe(nameof(MethodModel));
        missingComponent.Schema.Source?.Path.ShouldBe([nameof(TestPage), nameof(TestPage.GetData)]);
    }

    [Test]
    public void When_used_in_a_parameter__MissingComponent_contains_parameter_info()
    {
        var type = GiveMe.TheTypeModel<TestPage>().GetMembers();
        var method = type.Methods[nameof(TestPage.GetData)];
        var parameter = method.DefaultOverload.Parameters["panel"];
        var componentContext = GiveMe.AComponentContext(paths: ["page", "with-no-config"]);

        var component = parameter.GetRequiredComponent(componentContext);

        var missingComponent = component.ShouldBeOfType<ComponentDescriptor<MissingComponent>>();
        missingComponent.Schema.Source?.Type.ShouldBe(nameof(ParameterModel));
        missingComponent.Schema.Source?.Path.ShouldBe([nameof(TestPage), nameof(TestPage.GetData), "panel"]);
    }

    [Test]
    public void When_specific_component_was_required__MissingComponent_includes_component_info()
    {
        var type = GiveMe.TheTypeModel<TestPage>().GetMetadata();
        var componentContext = GiveMe.AComponentContext(paths: ["page", "with-no-config"]);

        var component = type.GetRequiredComponent<Text>(componentContext);

        var missingComponent = component.ShouldBeOfType<ComponentDescriptor<MissingComponent>>();
        missingComponent.Schema.Component.ShouldNotBeNull();
        missingComponent.Schema.Component.ShouldBe(nameof(Text));
    }
}