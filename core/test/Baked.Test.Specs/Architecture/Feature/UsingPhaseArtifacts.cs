using Baked.Architecture;

namespace Baked.Test.Architecture.Feature;

public class UsingPhaseArtifacts : ArchitectureSpec
{
    public record TargetD
    {
        public int Artifact { get; set; }
    }

    [Test]
    public void Feature_accesses_phase_artifacts_from_layer_configurator()
    {
        var target = new TargetD();
        var feature = new DFeature.Configurator().X();

        feature.Configure(GiveMe.ALayerConfigurator(
            target: target,
            context: GiveMe.AnApplicationContext(content: 5)
        ));

        target.Artifact.ShouldBe(5);
    }

    [Test]
    public void Feature_keeps_a_state_across_configurations_by_declaring_it_before_define()
    {
        var target = new TargetD();
        var feature = new DFeature.Configurator().X();

        feature.Configure(GiveMe.ALayerConfigurator(
            target: new TargetD(),
            context: GiveMe.AnApplicationContext(content: 5)
        ));
        feature.Configure(GiveMe.ALayerConfigurator(target: target));

        target.Artifact.ShouldBe(5);
    }
}

public static class DFeature
{
    public class Configurator : FeatureConfigurator<Configurator>;

    public static IFeature<Configurator> X(this Configurator x)
    {
        var artifact = 0;

        return x.Define(configurator =>
        {
            configurator.Configure((UsingPhaseArtifacts.TargetD target) =>
            {
                configurator.Use<int>(@int => artifact = @int);

                target.Artifact = artifact;
            });
        });
    }
}