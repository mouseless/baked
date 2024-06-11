using Baked.Architecture;

namespace Baked.Test.Architecture.Feature;

public class UsingPhaseArtifacts : ArchitectureSpec
{
    public class FeatureUsingPhaseArtifact : IFeature
    {
        public int Artifact { get; private set; }

        public void Configure(LayerConfigurator configurator)
        {
            configurator.Configure((string target) =>
            {
                Artifact = configurator.Context.Get<int>();
            });
        }
    }

    [Test]
    public void Feature_accesses_phase_artifacts_from_layer_configurator()
    {
        var configurator = GiveMe.ALayerConfigurator(
            target: GiveMe.AString(),
            context: GiveMe.AnApplicationContext(content: 5)
        );
        var feature = new FeatureUsingPhaseArtifact();

        feature.Configure(configurator);

        feature.Artifact.ShouldBe(5);
    }
}