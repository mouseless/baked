namespace Baked.Test.Ui;

// TODO - review this in form components
public record Action
{
    public string? Path { get; set; }
    public string Method { get; set; } = "POST";
}