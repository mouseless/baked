namespace Baked.Ui;

public record IsNotConstraint(object IsNot)
    : IConstraint
{
    public string Type => nameof(IsNot);
    public object IsNot { get; set; } = IsNot;
}