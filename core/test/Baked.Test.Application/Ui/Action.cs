namespace Baked.Test.Ui;

public record Action
{
    public string? Path { get; set; }
    public string Method { get; set; } = "POST";
}