namespace Baked.Ui;

public class CompositeTrigger : ITrigger
{
    public string Type => "Composite";
    public List<ITrigger> Parts { get; init; } = [];
}