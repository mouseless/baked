namespace Baked.Ui;

public record CompositeAction : IAction
{
    public string Type => "Composite";
    public bool? IgnoreOnEmpty { get; set; }
    public List<IAction> Parts { get; init; } = [];
}