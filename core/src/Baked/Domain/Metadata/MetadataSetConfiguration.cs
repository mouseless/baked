namespace Baked.Domain.Metadata;

public class MetadataSetConfiguration(string name)
{
    public string Name => name;
    public MetadataModelBuilderOptions BuilderOptions { get; } = new();

    public void ConfigureBuilderOptions(Action<MetadataModelBuilderOptions> configure) =>
        configure(BuilderOptions);
}