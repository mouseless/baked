using Baked.Domain.Model;
using Baked.Playground.Business;
using Baked.Playground.Theme;
using Baked.Ui;

namespace Baked.Test.Theme;

public class AddingMissingComponentWhenNoComponentWasConfigured : TestSpec
{
    [Test]
    public void When_no_component_is_found__it_returns_MissingComponent()
    {
        using var _ = Diagnostics.Start("test", _ => { });
        var type = GiveMe.TheTypeModel<TestPage>().GetMetadata();
        var componentContext = GiveMe.AComponentContext(paths: ["page", "with-no-config"]);

        var component = type.GetRequiredComponent(componentContext);

        component.ShouldBeOfType<ComponentDescriptor<MissingComponent>>();
    }

    [Test]
    public void When_used__MissingComponent_leaves_an_error_log_in_diagnostics_messages()
    {
        DiagnosticsResult? result = default;
        using (Diagnostics.Start("test", dr => result = dr))
        {
            var type = GiveMe.TheTypeModel<TestPage>().GetMetadata();
            var componentContext = GiveMe.AComponentContext(paths: ["page", "with-no-config"]);

            type.GetRequiredComponent(componentContext);
        }

        result?.Messages.ShouldContain(
            "error B0101:" +
            " `TestPage` doesn't have any component descriptor at path `/page/with-no-config`" +
            " (See: https://baked.mouseless.codes/errors#missing-required-component)"
        );
    }

    [Test]
    public void MissingComponent_contains_component_path_to_build_a_sample_config_code()
    {
        using var _ = Diagnostics.Start("test", _ => { });
        var type = GiveMe.TheTypeModel<TestPage>().GetMetadata();
        var componentContext = GiveMe.AComponentContext(paths: ["page", "with-no-config"]);

        var component = type.GetRequiredComponent(componentContext);

        var missingComponent = component.ShouldBeOfType<ComponentDescriptor<MissingComponent>>();
        missingComponent.Schema.Path.ShouldBe(["page", "with-no-config"]);
    }

    [Test]
    public void When_used_in_a_type__MissingComponent_contains_type_info()
    {
        using var _ = Diagnostics.Start("test", _ => { });
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
        using var _ = Diagnostics.Start("test", _ => { });
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
        using var _ = Diagnostics.Start("test", _ => { });
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
        using var _ = Diagnostics.Start("test", _ => { });
        var type = GiveMe.TheTypeModel<TestPage>().GetMembers();
        var method = type.Methods[nameof(TestPage.GetData)];
        var parameter = method.DefaultOverload.Parameters["panel"];
        var componentContext = GiveMe.AComponentContext(paths: ["page", "with-no-config"]);

        var component = parameter.GetRequiredComponent(componentContext);

        var missingComponent = component.ShouldBeOfType<ComponentDescriptor<MissingComponent>>();
        missingComponent.Schema.Source?.Type.ShouldBe(nameof(ParameterModel));
        missingComponent.Schema.Source?.Path.ShouldBe([nameof(TestPage), nameof(TestPage.GetData), "panel"]);
    }
}