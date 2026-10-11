namespace Baked.Ui;

public record IsConstraint : IConstraint
{
    public string Type => nameof(Is);
    public required object? Is { get; set; }
    public bool? Null => Is is null ? true : null;
}