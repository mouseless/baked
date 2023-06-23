namespace Do.Architecture;

public interface ILayer
{
    void Configure(object context);
    object GetConfigurationTarget();
}
