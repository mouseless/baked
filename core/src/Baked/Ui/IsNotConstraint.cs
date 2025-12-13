namespace Baked.Ui;

public record IsNotConstraint(string IsNot)
    : IConstraint
{
    public string Type => nameof(IsNot);
    public string IsNot { get; set; } = IsNot;
}