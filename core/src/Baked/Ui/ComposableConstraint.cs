namespace Baked.Ui;

public record ComposableConstraint(string Composable)
    : IConstraint
{
    public string Type => nameof(Composable);
    public string Composable { get; set; } = Composable;
    public IData? Options { get; set; }
}