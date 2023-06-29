namespace Do.Architecture;

public interface ILayer
{
    void Configure(List<string> phases);
    object GetConfigurationTarget();
}
