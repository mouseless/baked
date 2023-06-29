namespace Do.Architecture;

public class Application : IRunnable
{
    List<IPhase> Phases { get; } = new();
    public List<ILayer> Layers { get; } = new();
    public List<IFeature> Features { get; } = new();

    void IRunnable.Run()
    {
        var context = new ApplicationContext();

        foreach (var layer in Layers)
        {
            Phases.AddRange(layer.GetPhases());
        }

        // find and remove ready phases, iterate until all phases are applied
        foreach (var phase in Phases)
        {
            context.Phase = phase;
            phase.Initialize(context);

            foreach (var layer in Layers)
            {
                var target = layer.GetConfigurationTarget(context);
                foreach (var feature in Features)
                {
                    feature.Configure(target);
                }
            }
        }
    }
}
