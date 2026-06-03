namespace Baked.Ui;

public record CompositeTrigger : ITrigger
{
    public string Type => "Composite";
    public List<ITrigger> Parts { get; init; } = [];
}