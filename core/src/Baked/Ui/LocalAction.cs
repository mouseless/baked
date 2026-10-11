namespace Baked.Ui;

public record LocalAction : IAction
{
    public string Type => "Local";
    public string Composable { get; set; } = string.Empty;
    public IData? Options { get; set; }
}