namespace Do.Architecture;

public class CannotProceedException : Exception
{
    public CannotProceedException(IEnumerable<IPhase> phases)
        : base(
            "Cannot proceed to run the application. " +
            $"Phases ({string.Join(", ", phases.Select(p => p.GetType().Name))}) " +
            "won't get ready for initialization."
        )
    { }
}
