namespace Baked.Ui;

public record IsConstraint(string Is)
    : IConstraint
{
    public string Is { get; set; } = Is;
}