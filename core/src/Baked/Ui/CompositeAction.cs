namespace Baked.Ui;

public record CompositeAction : IAction
{
    public string Type => "Composite";
    public List<IAction> Parts { get; init; } = [];
}