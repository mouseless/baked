namespace Do.Architecture;

public class PhaseOrderException : Exception
{
    public PhaseOrderException(PhaseOrder order, IEnumerable<IPhase> phases) :
        base($"More than one phase cannot have '{order}' at the same time. " +
            "Change the order of phases. Conflicting phases are: " +
            $"'{string.Join(", ", phases.Where(p => p.Order == order).Select(p => p.GetType().Name))}'"
        )
    { }
}
