namespace Baked.Ui;

public record IsConstraint(object? Is)
    : IConstraint
{
    public string Type => nameof(Is);
    public object? Is { get; set; } = Is;
    public bool? Null => Is is null ? true : null;
}