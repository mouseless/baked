namespace Baked.Ui;

public record ComposableConstraint(string Composable)
{
    public string Composable { get; set; } = Composable;
    public IData? Options { get; set; }
}