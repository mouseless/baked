namespace Do.Architecture;

public class Application
{
    readonly ApplicationContext _context;

    public Application(ApplicationContext context) => _context = context;

    readonly List<ILayer> _layers = new();
    readonly List<IFeature> _features = new();
    readonly List<IPhase> _phases = new();

    internal Application With(ApplicationDescriptor descriptor)
    {
        _layers.AddRange(descriptor.Layers);
        _features.AddRange(descriptor.Features);

        FillPhases();

        return this;
    }

    void FillPhases()
    {
        foreach (var layer in _layers)
        {
            _phases.AddRange(layer.GetPhases());
        }

        _phases.Sort((l, r) => l.Order - r.Order);
    }

    public void Run()
    {
        var retryCount = 0;
        var phases = new List<IPhase>(_phases);

        do
        {
            var orderOfLastInitializedPhase = PhaseOrder.Normal;
            var initializedPhases = new HashSet<IPhase>();

            foreach (var phase in phases)
            {
                var initialized = phase.Initialize(_context);

                if (!initialized) { continue; }

                initializedPhases.Add(phase);

                if (orderOfLastInitializedPhase is PhaseOrder.Earliest or PhaseOrder.Latest &&
                    orderOfLastInitializedPhase == phase.Order)
                {
                    throw new PhaseOrderException(orderOfLastInitializedPhase, initializedPhases);
                }

                Apply(phase);

                orderOfLastInitializedPhase = phase.Order;
            }

            phases.RemoveAll(p => initializedPhases.Contains(p));

            if (retryCount++ > 100) { throw new InvalidOperationException("max retry count exceeded"); }
        } while (phases.Count > 0);
    }

    void Apply(IPhase phase)
    {
        foreach (var layer in _layers)
        {
            var target = layer.GetConfigurationTarget(phase, _context);
            foreach (var feature in _features)
            {
                feature.Configure(target);
            }
        }
    }
}
