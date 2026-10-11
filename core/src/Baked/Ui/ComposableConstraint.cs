namespace Baked.Ui;

public record ComposableConstraint
    : IConstraint
{
    public string Type => nameof(Composable);
    public required string Composable { get; set; }
    public IData? Options { get; set; }
}