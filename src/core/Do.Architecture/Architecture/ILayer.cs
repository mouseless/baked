namespace Do.Architecture;

public interface ILayer
{
    void ConfigurePhases(List<string> phases);
    void ApplyPhase(string phase);
}
