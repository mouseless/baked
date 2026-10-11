namespace Baked.Ui;

public record OnTrigger : ITrigger
{
    public string Type => nameof(On);
    public required string On { get; set; }
    public IConstraint? Constraint { get; set; }
}