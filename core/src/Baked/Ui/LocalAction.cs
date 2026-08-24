namespace Baked.Ui;

public record LocalAction(string Composable)
    : IAction
{
    public string Type => "Local";
    public bool? IgnoreOnEmpty { get; set; }
    public string Composable { get; set; } = Composable;
    public IData? Options { get; set; }
}