namespace Baked.CodeGeneration;

public record GeneratedFileDescriptor(string Name)
{
    public string Content { get; init; } = string.Empty;
    public string Extension { get; init; } = "txt";
    public string? Outdir { get; init; }
}