namespace Baked.Ui;

public record OnTrigger(string On)
    : ITrigger
{
    public string On { get; set; } = On;
    public IConstraint? Constraint { get; set; }
}