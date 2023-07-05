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
        var phases = new List<IPhase>(_phases);
        while (phases.Count > 0)
        {
            var phasesOfThisIteration = phases.Where(p => p.IsReady(_context)).ToList();
            if (!phasesOfThisIteration.Any()) { throw new CannotProceedException(phases); }

            VerifyOrderOccursAtMostOnce(PhaseOrder.Earliest, phasesOfThisIteration);
            VerifyOrderOccursAtMostOnce(PhaseOrder.Latest, phasesOfThisIteration);

            foreach (var phase in phasesOfThisIteration)
            {
                phase.Initialize(_context);

                Apply(phase);
            }

            phases = phases.Except(phasesOfThisIteration).ToList();
        }
    }

    void VerifyOrderOccursAtMostOnce(PhaseOrder order, IEnumerable<IPhase> phases)
    {
        var phasesWithOrder = phases.Where(p => p.Order == order);
        if (phasesWithOrder.Count() > 1)
        {
            throw new OverlappingPhaseException(order, phasesWithOrder);
        }
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
