namespace Baked.Domain.Metadata;

public class MetadataSetConfiguration(string name)
{
    public string Name => name;
    internal MetadataModelBuilderOptions BuilderOptions { get; } = new();

    public void ConfigureMetadata(Action<MetadataModelBuilderOptions> configure) =>
        configure(BuilderOptions);
}