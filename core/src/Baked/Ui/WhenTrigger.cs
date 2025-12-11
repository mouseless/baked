namespace Baked.Ui;

public record WhenTrigger(string When)
    : ITrigger
{
    public string When { get; set; } = When;
    public IConstraint? Constraint { get; set; }
}