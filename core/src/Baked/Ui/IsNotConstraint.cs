namespace Baked.Ui;

public record IsNotConstraint(string IsNot)
    : IConstraint
{
    public string IsNot { get; set; } = IsNot;
}