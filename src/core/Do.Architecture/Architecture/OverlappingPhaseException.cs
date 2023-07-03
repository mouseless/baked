namespace Do.Architecture;

public class OverlappingPhaseException : Exception
{
    public OverlappingPhaseException(PhaseOrder order, IEnumerable<IPhase> phases)
        : base(
            $"More than one phase cannot have '{order}' at the same time. " +
            "Change the order of phases. Overlapping phases are: " +
            $"{string.Join(", ", phases.Where(p => p.Order == order).Select(p => p.GetType().Name))}"
        )
    { }
}
