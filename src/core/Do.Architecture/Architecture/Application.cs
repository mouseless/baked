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
        CheckDuplicates(descriptor.Layers.Select(layer => layer.Id));
        _layers.AddRange(descriptor.Layers);

        CheckDuplicates(descriptor.Features.Select(feature => feature.Id));
        _features.AddRange(descriptor.Features);

        FillPhases();

        return this;
    }

    void CheckDuplicates(IEnumerable<string> ids)
    {
        var duplicate = ids.GroupBy(x => x).Where(g => g.Count() > 1).Select(l => l.Key).FirstOrDefault();

        if (duplicate is not null)
        {
            throw new InvalidOperationException($"Cannot add '{duplicate}', it was already added.");
        }
    }

    void FillPhases()
    {
        foreach (var layer in _layers)
        {
            _phases.AddRange(layer.GetPhases());
        }

        _phases.ForEach(p => p.Context = _context);
        _phases.Sort((l, r) => l.Order - r.Order);
    }

    public void Run()
    {
        var phases = new List<IPhase>(_phases);
        while (phases.Count > 0)
        {
            var phasesOfThisIteration = phases.Where(p => p.IsReady).ToList();
            if (!phasesOfThisIteration.Any()) { throw new CannotProceedException(phases); }

            VerifyOrderOccursAtMostOnce(PhaseOrder.Earliest, phasesOfThisIteration);
            VerifyOrderOccursAtMostOnce(PhaseOrder.Latest, phasesOfThisIteration);

            foreach (var phase in phasesOfThisIteration)
            {
                phase.Initialize();

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
        var contexts = _layers.Select(layer => layer.GetContext(phase, _context)).ToList();
        foreach (var context in contexts)
        {
            foreach (var configurator in context.Configurators)
            {
                foreach (var feature in _features)
                {
                    feature.Configure(configurator);
                }
            }
        }

        foreach (var context in contexts.Cast<IDisposable>())
        {
            context.Dispose();
        }
    }
}
