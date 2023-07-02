namespace Do.Architecture;

public class CannotProceedException : Exception
{
    public CannotProceedException(IEnumerable<IPhase> phases)
        : base(
            "Cannot proceed to run the application. " +
            $"All of these phases '{string.Join(", ", phases.Select(p => p.GetType().Name))}' " +
            "return false after Initialize()."
        )
    { }
}
