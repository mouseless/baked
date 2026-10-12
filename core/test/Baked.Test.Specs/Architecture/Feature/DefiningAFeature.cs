using Baked.Architecture;

namespace Baked.Test.Architecture.Feature;

public class DefiningAFeature : ArchitectureSpec
{
    [Test]
    public void A_feature_implementation_is_defined_by_an_extension_method_of_its_abstraction_configurator()
    {
        var feature = new AFeature.Configurator().X();

        feature.ShouldBeAssignableTo<IFeature<AFeature.Configurator>>();
    }

    [Test]
    public void All_feature_implementations_implement_IFeature_interface_behind_the_scenes()
    {
        var feature = new AFeature.Configurator().X();

        feature.ShouldBeAssignableTo<IFeature>();
    }

    [Test]
    public void By_default__id_of_a_feature_is_the_name_of_the_method_defining_it_prefixed_with_its_abstraction_name()
    {
        var feature = new AFeature.Configurator().X();

        feature.Id.ShouldBe("A.X");
    }

    [Test]
    public void Feature_id_can_be_given_explicitly_when_defining_a_feature_inline()
    {
        var feature = new AFeature.Configurator().Define("Y", configurator => { });

        feature.Id.ShouldBe("A.Y");
    }

    [Test]
    public void Feature_configurator_provides_a_disabled_feature_to_allow_a_disabled_option()
    {
        var feature = new AFeature.Configurator().Disabled();

        feature.ShouldBeAssignableTo<IFeature<AFeature.Configurator>>();
        feature.Id.ShouldBe("A.Disabled");
    }

    [Test]
    public void Features_defined_with_the_same_name_in_different_abstractions_have_different_ids()
    {
        var featureA = new AFeature.Configurator().Disabled();
        var featureB = new BFeature.Configurator().Disabled();

        featureA.Id.ShouldNotBe(featureB.Id);
    }
}

public static class AFeature
{
    public class Configurator : FeatureConfigurator<Configurator>;

    public static IFeature<Configurator> X(this Configurator x) =>
        x.Define(configurator => { });
}

public static class BFeature
{
    public class Configurator : FeatureConfigurator<Configurator>;
}