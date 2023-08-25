using Do.Architecture;

namespace Do.Test.Architecture.Feature;

public class CreatingAFeature : ArchitectureSpec
{
    public class FeatureConfigurator { }

    public class Feature : IFeature<FeatureConfigurator>
    {
        public void Configure(LayerConfigurator configurator) { }
    }

    public class FeatureOverriddenId : IFeature<FeatureConfigurator>
    {
        public string Id => "Test";

        public void Configure(LayerConfigurator configurator) { }
    }

    [Test]
    public void Feature_default_id_is_its_name()
    {
        IFeature feature = new Feature();

        feature.Id.ShouldBe("Feature");
    }

    [Test]
    public void Feature_id_can_be_overridden()
    {
        IFeature feature = new FeatureOverriddenId();

        feature.Id.ShouldBe("Test");
    }
}
