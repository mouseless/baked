using Do.Architecture;

namespace Do.Test.Architecture.Feature;

public class UsingPhaseArtifacts : ArchitectureSpec
{
    public record LayerConfiguration
    {
        public string? Value { get; set; }
    }

    public record PhaseArtifact(string Value);

    public class FeatureUsingPhaseArtifact : IFeature
    {
        readonly Action<LayerConfigurator, LayerConfiguration> _configureAction;

        public FeatureUsingPhaseArtifact(Action<LayerConfigurator, LayerConfiguration> configureAction)
        {
            _configureAction = configureAction;
        }

        public void Configure(LayerConfigurator configurator)
        {
            configurator.Configure((LayerConfiguration configuration) =>
            {
                _configureAction(configurator, configuration);
            });
        }
    }

    [Test]
    public void Feature_can_access_phase_artifacts_from_layer_configurator_and_can_use_them_when_configuring_a_layer()
    {
        var applicationContext = GiveMe.AnApplicationContext(content: new PhaseArtifact("Value from phase artifact"));

        var configuration = new LayerConfiguration();
        var configuratior = GiveMe.ALayerConfigurator(target: configuration, context: applicationContext);

        var feature = new FeatureUsingPhaseArtifact((configurator, configuration) =>
        {
            var artifact = configurator.Context.Get<PhaseArtifact>();

            configuration.Value = artifact.Value;
        });

        feature.Configure(configuratior);

        configuration.Value.ShouldBe("Value from phase artifact");
    }

    [Test]
    [Ignore("not-implemented")]
    public void Only_a_phase_can_add_an_artifact_to_application_context() => this.ShouldFail();
}
