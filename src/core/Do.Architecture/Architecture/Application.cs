namespace Do.Architecture;

public class Application : IRunnable
{
    readonly ApplicationContext _context;

    public Application(ApplicationContext context) => _context = context;

    public List<ILayer> Layers { get; } = new();
    public List<IFeature> Features { get; } = new();

    void IRunnable.Run()
    {
        var phases = new List<IPhase>();
        foreach (var layer in Layers)
        {
            phases.AddRange(layer.GetPhases());
        }

        var retryCount = 0;
        do
        {
            var retry = new HashSet<IPhase>();
            foreach (var phase in phases)
            {
                if (!phase.Initialize(_context))
                {
                    retry.Add(phase);
                    continue;
                }

                foreach (var layer in Layers)
                {
                    var target = layer.GetConfigurationTarget(phase, _context);
                    foreach (var feature in Features)
                    {
                        feature.Configure(target);
                    }
                }
            }

            phases.RemoveAll(p => !retry.Contains(p));

            if (retryCount++ > 100) { throw new InvalidOperationException("max retry count exceeded"); }
        } while (phases.Count > 0);
    }
}
