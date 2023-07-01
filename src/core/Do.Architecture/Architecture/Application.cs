namespace Do.Architecture;

public class Application : IRunnable
{
    readonly ApplicationContext _context;

    public Application(ApplicationContext context) => _context = context;

    List<IPhase> Phases { get; } = new();
    public List<ILayer> Layers { get; } = new();
    public List<IFeature> Features { get; } = new();

    void IRunnable.Run()
    {
        foreach (var layer in Layers)
        {
            Phases.AddRange(layer.GetPhases());
        }

        // find and remove ready phases, iterate until all phases are applied
        foreach (var phase in Phases)
        {
            phase.Initialize(_context);

            foreach (var layer in Layers)
            {
                var target = layer.GetConfigurationTarget(phase, _context);
                foreach (var feature in Features)
                {
                    feature.Configure(target);
                }
            }
        }
    }
}
