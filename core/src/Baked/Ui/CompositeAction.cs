namespace Baked.Ui;

public record CompositeAction : IAction
{
    public string Type => "Composite";
    public List<IAction> Parts { get; init; } = [];
    public bool? IgnoreOnEmpty
    {
        get =>
            Parts.Count > 0 && Parts.All(p => p.IgnoreOnEmpty == true)
                ? true
                : null;
        set { }
    }
}