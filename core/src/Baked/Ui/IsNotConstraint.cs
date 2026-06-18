namespace Baked.Ui;

public record IsNotConstraint(object? IsNot)
    : IConstraint
{
    public string Type => nameof(IsNot);
    public object? IsNot { get; set; } = IsNot;
    public bool? Null => IsNot is null ? true : null;
}