﻿using System.Text.RegularExpressions;

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
        CheckDuplicates(descriptor);
        _layers.AddRange(descriptor.Layers);
        _features.AddRange(descriptor.Features);

        FillPhases();

        return this;
    }

    void CheckDuplicates(ApplicationDescriptor descriptor)
    {
        try
        {
            descriptor.Layers.ToDictionary(
                keySelector: layer => layer.Id,
                elementSelector: layer => layer
            );
            descriptor.Features.ToDictionary(
                keySelector: feature => feature.Id,
                elementSelector: feature => feature
            );
        }
        catch (ArgumentException e)
        {
            // It takes the name value after "Key:" in the exeception message.
            string pattern = @"Key:\s*(.*?)(?:\s|$)";

            Match match = Regex.Match(e.Message, pattern);

            string key = string.Empty;
            if (match.Success)
            {
                key = match.Groups[1].Value;
            }

            throw new InvalidOperationException($"Cannot add `{key}`, it was already added.");
        }
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
