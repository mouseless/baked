namespace Baked.Architecture;

public class Application(ApplicationContext _context,
    ApplicationContext? _bakeContext = default
)
{
    readonly List<ILayer> _layers = [];
    readonly List<IFeature> _features = [];
    readonly List<IPhase> _phases = [];
    readonly List<IPhase> _bakePhases = [];
    RunFlags _runFlags = RunFlags.Start!;

    internal Application With(ApplicationDescriptor descriptor, RunFlags runFlags)
    {
        _runFlags = runFlags;

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
        if (_runFlags.HasFlag(RunFlags.Bake))
        {
            foreach (var layer in _layers)
            {
                _bakePhases.AddRange(layer.GetBakePhases());
            }

            _bakeContext ??= new();
            _bakePhases.ForEach(p => p.Context = _bakeContext);
            _bakePhases.Sort((l, r) => l.Order - r.Order);
        }

        if (_runFlags.HasFlag(RunFlags.Start))
        {
            foreach (var layer in _layers)
            {
                _phases.AddRange(layer.GetPhases());
            }

            _phases.ForEach(p => p.Context = _context);
            _phases.Sort((l, r) => l.Order - r.Order);
        }
    }

    public void Run()
    {
        if (_runFlags.HasFlag(RunFlags.Bake))
        {
            ExecutePhases(_bakePhases, _bakeContext ?? new());
        }

        if (_runFlags.HasFlag(RunFlags.Start))
        {
            ExecutePhases(_phases, _context);
        }
    }

    void ExecutePhases(List<IPhase> phases, ApplicationContext context)
    {
        while (phases.Count > 0)
        {
            var phasesOfThisIteration = phases.Where(p => p.IsReady).ToList();
            if (!phasesOfThisIteration.Any()) { throw new CannotProceedException(phases); }

            VerifyOrderOccursAtMostOnce(PhaseOrder.Earliest, phasesOfThisIteration);
            VerifyOrderOccursAtMostOnce(PhaseOrder.Latest, phasesOfThisIteration);

            foreach (var phase in phasesOfThisIteration)
            {
                phase.Initialize();

                Apply(phase, context);
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

    void Apply(IPhase phase, ApplicationContext applicationContext)
    {
        var contexts = _layers.Select(layer => layer.GetContext(phase, applicationContext)).ToList();
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