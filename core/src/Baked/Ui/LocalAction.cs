namespace Baked.Ui;

public record LocalAction(string Composable) : IAction
{
    public string Type => "Local";
    public string Composable { get; set; } = Composable;
    public List<object> Args { get; init; } = [];
}