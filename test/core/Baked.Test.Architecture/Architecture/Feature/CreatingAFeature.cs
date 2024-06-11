using Baked.Architecture;

namespace Baked.Test.Architecture.Feature;

public class CreatingAFeature : ArchitectureSpec
{
    public class FeatureAConfigurator { }

    public class FeatureA : IFeature<FeatureAConfigurator>
    {
        public void Configure(LayerConfigurator configurator) { }
    }

    [Test]
    public void A_feature_implementation_is_created_by_implementing_generic_IFeature_interface_using_its_configurator()
    {
        var feature = new FeatureA();

        feature.ShouldBeAssignableTo<IFeature<FeatureAConfigurator>>();
    }

    [Test]
    public void All_feature_implementations_implements_IFeature_interface_behind_the_scenes()
    {
        var feature = new FeatureA();

        feature.ShouldBeAssignableTo<IFeature>();
    }

    [Test]
    public void By_default__id_of_a_feature_is_its_class_name()
    {
        var feature = new FeatureA() as IFeature<FeatureAConfigurator>;

        feature.Id.ShouldBe(nameof(FeatureA));
    }

    public class FeatureBConfigurator { }

    public class FeatureB : IFeature<FeatureBConfigurator>
    {
        public string Id => "B Feature";

        public void Configure(LayerConfigurator configurator) { }
    }

    [Test]
    public void Feature_id_can_be_overridden_by_implementing_an_Id_property_in_feature_class()
    {
        var feature = new FeatureB() as IFeature<FeatureBConfigurator>;

        feature.Id.ShouldBe("B Feature");
    }

    public class FeatureCConfigurator
    {
        public IFeature<FeatureCConfigurator> Disabled() => Baked.Architecture.Feature.Empty<FeatureCConfigurator>();
    }

    [Test]
    public void Feature_configurator_can_provide_an_empty_feature_to_allow_a_disabled_option()
    {
        var configurator = new FeatureCConfigurator();

        var feature = configurator.Disabled();

        feature.ShouldBeEquivalentTo(Baked.Architecture.Feature.Empty<FeatureCConfigurator>());
    }

    [Test]
    public void Empty_feature_ids_are_unique_by_their_configurator_names()
    {
        var featureC = Baked.Architecture.Feature.Empty<FeatureCConfigurator>();
        var featureB = Baked.Architecture.Feature.Empty<FeatureBConfigurator>();

        featureC.Id.ShouldNotBe(featureB.Id);
    }
}