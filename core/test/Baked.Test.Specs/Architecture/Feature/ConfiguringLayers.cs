using Baked.Architecture;

namespace Baked.Test.Architecture.Feature;

public class ConfiguringLayers : ArchitectureSpec
{
    public record ConfigurationA
    {
        public string? Value { get; set; }
    }

    [Test]
    public void Features_configure_layer_using_a_layer_configurator_that_can_apply_given_action_to_its_target_object()
    {
        var configuration = new ConfigurationA();
        var configurator = GiveMe.ALayerConfigurator(target: configuration);

        var feature = new CFeature.Configurator().X("test");

        feature.Configure(configurator);

        configuration.Value.ShouldBe("test");
    }

    [Test]
    public void Feature_options_are_given_to_define_so_that_configure_action_receives_them_with_defaults_applied()
    {
        var configurationWithDefault = new ConfigurationA();
        var configurationWithValue = new ConfigurationA();

        new CFeature.Configurator().Y().Configure(GiveMe.ALayerConfigurator(target: configurationWithDefault));
        new CFeature.Configurator().Y(value: "test").Configure(GiveMe.ALayerConfigurator(target: configurationWithValue));

        configurationWithDefault.Value.ShouldBe("default");
        configurationWithValue.Value.ShouldBe("test");
    }

    [Test]
    public void Arguments_given_to_define_are_evaluated_once_and_shared_across_configurations()
    {
        var configuration = new ConfigurationA();
        var feature = new CFeature.Configurator().Z();

        feature.Configure(GiveMe.ALayerConfigurator(target: configuration));
        feature.Configure(GiveMe.ALayerConfigurator(target: configuration));

        configuration.Value.ShouldBe("2");
    }

    [Test]
    public void Disabled_feature_does_not_configure_layers()
    {
        var configuration = new ConfigurationA();
        var configurator = GiveMe.ALayerConfigurator(target: configuration);

        var disabled = new CFeature.Configurator().Disabled();

        disabled.Configure(configurator);

        configuration.Value.ShouldBeNull();
    }

    [Test]
    public void Layer_configurator_does_not_apply_given_action_when_given_type_does_not_match_current_target_type()
    {
        var configurator = GiveMe.ALayerConfigurator(target: new ConfigurationA());

        configurator.Configure((object value) => this.ShouldFail());

        this.ShouldPass("didn't configure as expected");
    }

    [Test]
    public void Layer_configurator_can_restrict_a_target_into_one_of_its_base_class_or_interfaces()
    {
        var configurator = GiveMe.ALayerConfigurator<object>(target: new ConfigurationA());

        var configured = false;
        configurator.Configure((object value) => configured = true);
        configurator.Configure((ConfigurationA value) => configured = false);

        configured.ShouldBeTrue("should've configured only for the first call");
    }

    [Test]
    public void Layer_configurator_accepts_two_parameters_in_a_given_action()
    {
        var configurator = GiveMe.ALayerConfigurator(target1: "test", target2: 10);

        var configured = false;
        configurator.Configure((string str, int i) => configured = true);
        configurator.Configure((object str, object i) => configured = false);

        configured.ShouldBeTrue("should've configured only for the first call");
    }

    [Test]
    public void Layer_configurator_accepts_three_parameters_in_a_given_action()
    {
        var configurator = GiveMe.ALayerConfigurator(target1: "test", target2: 10, target3: false);

        var configured = false;
        configurator.Configure((string str, int i, bool b) => configured = true);
        configurator.Configure((object str, object i, object b) => configured = false);

        configured.ShouldBeTrue("should've configured only for the first call");
    }
}

public static class CFeature
{
    public class Configurator : FeatureConfigurator<Configurator>;

    public static IFeature<Configurator> X(this Configurator x,
        string value
    ) => x.Define(configurator =>
    {
        configurator.Configure((ConfiguringLayers.ConfigurationA configuration) => configuration.Value = value);
    });

    public static IFeature<Configurator> Y(this Configurator x,
        string? value = default
    ) => x.Define(value ?? "default", (configurator, value) =>
    {
        configurator.Configure((ConfiguringLayers.ConfigurationA configuration) => configuration.Value = value);
    });

    public static IFeature<Configurator> Z(this Configurator x) => x.Define(new List<string>(), (configurator, calls) =>
    {
        configurator.Configure((ConfiguringLayers.ConfigurationA configuration) =>
        {
            calls.Add(nameof(Z));
            configuration.Value = $"{calls.Count}";
        });
    });
}