using Do.Architecture;
using Shouldly;

namespace Do.Test.Architecture.Feature;

public class ConfiguringLayers : Spec
{
    public record ConfigurationA
    {
        public string? Value { get; set; }
    }

    public class FeatureA : IFeature
    {
        readonly string _value;

        public FeatureA(string value) => _value = value;

        public void Configure(LayerConfigurator configurator)
        {
            configurator.Configure((ConfigurationA configuration) => configuration.Value = _value);
        }
    }

    [Test]
    public void Features_configure_layer_using_a_layer_configurator_that_can_apply_given_action_to_its_target_object()
    {
        var configuration = new ConfigurationA();
        var configurator = GiveMe.ALayerConfigurator(configuration);

        var featureA = new FeatureA("test");

        featureA.Configure(configurator);

        configuration.Value.ShouldBe("test");
    }

    [Test]
    public void Layer_configurator_does_not_apply_given_action_when_given_type_does_not_match_current_target_type()
    {
        var configurator = LayerConfigurator.Create(new ConfigurationA());

        configurator.Configure((object value) => Assert.Fail());

        Assert.Pass("didn't configure as expected");
    }

    [Test]
    public void Layer_configurator_can_restrict_a_target_into_one_of_its_base_class_or_interfaces()
    {
        var configurator = LayerConfigurator.Create<object>(new ConfigurationA());

        var configured = false;
        configurator.Configure((object value) => configured = true);
        configurator.Configure((ConfigurationA value) => configured = false);

        configured.ShouldBeTrue("should've configured only for the first call");
    }

    [Test]
    public void Layer_configurator_accepts_two_parameters_in_a_given_action()
    {
        var configurator = LayerConfigurator.Create<string, int>("test", 10);

        var configured = false;
        configurator.Configure((string str, int i) => configured = true);
        configurator.Configure((object str, object i) => configured = false);

        configured.ShouldBeTrue("should've configured only for the first call");
    }

    [Test]
    public void Layer_configurator_accepts_three_parameters_in_a_given_action()
    {
        var configurator = LayerConfigurator.Create<string, int, bool>("test", 10, false);

        var configured = false;
        configurator.Configure((string str, int i, bool b) => configured = true);
        configurator.Configure((object str, object i, object b) => configured = false);

        configured.ShouldBeTrue("should've configured only for the first call");
    }

    public class FeatureAConfigurator
    {
        public IFeature Disabled() => Do.Architecture.Feature.Empty;
    }

    [Test]
    public void Feature_configurator_can_provide_an_empty_feature_to_allow_a_disable_option()
    {
        var configurator = new FeatureAConfigurator();

        var feature = configurator.Disabled();

        feature.ShouldBe(Do.Architecture.Feature.Empty);
    }
}
