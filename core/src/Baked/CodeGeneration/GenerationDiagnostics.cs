namespace Baked.CodeGeneration;

public class GenerationDiagnostics(
    Action<GenerationDiagnostics>? onComplete = default
)
{
    Action<GenerationDiagnostics> _complete =
        onComplete ?? (
            d =>
            {
                if (d.Errors.Any())
                {
                    Environment.Exit(1);
                }
            }
        );

    public List<Exception> Errors { get; } = [];

    public void OnComplete(Action<GenerationDiagnostics> handler) =>
        _complete = handler;

    public void Diagnose(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            Errors.Add(ex);
        }
    }

    public void Complete() => _complete(this);
}