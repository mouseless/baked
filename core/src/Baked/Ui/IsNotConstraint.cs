namespace Baked.Ui;

public record IsNotConstraint : IConstraint
{
    public string Type => nameof(IsNot);
    public required object? IsNot { get; set; }
    public bool? Null => IsNot is null ? true : null;
}