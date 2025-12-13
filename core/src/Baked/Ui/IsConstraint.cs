namespace Baked.Ui;

public record IsConstraint(string Is)
    : IConstraint
{
    public string Type => nameof(Is);
    public string Is { get; set; } = Is;
}