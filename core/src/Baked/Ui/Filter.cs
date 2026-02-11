namespace Baked.Ui;

public record Filter : IComponentSchema
{
    public string? Placeholder { get; set; }
    public bool? IgnoreWhiteSpace { get; set; }
}