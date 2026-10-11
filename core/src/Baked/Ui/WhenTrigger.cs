namespace Baked.Ui;

public record WhenTrigger : ITrigger
{
    public string Type => nameof(When);
    public required string When { get; set; }
    public string? Key { get; set; }
    public IConstraint? Constraint { get; set; }
}